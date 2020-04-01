using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using APD.Networking.Entities;
using APD.Networking.Utilities;

namespace APD.Networking
{
    public class Server
    {
        private int madeConnections = 0;

        private readonly TcpListener tcpListener;
        private readonly Dictionary<string, Listener> listeners;
        private readonly Thread threadNewConnections;
        private readonly MessageMapper messageMapper;

        public int Port { get; }

        public delegate void ClientConnected(string username);
        public ClientConnected OnClientConnected { get; set; } = delegate(string username) {  };

        public delegate void ClientDisconnected(string username);
        public ClientDisconnected OnClientDisconnected { get; set; } = delegate (string username) { };

        public Server(int port)
        {
            // TODO: Check if port is already used
            Port = port;

            tcpListener = new TcpListener(System.Net.IPAddress.Any, port);
            tcpListener.Start();

            listeners = new Dictionary<string, Listener>();

            threadNewConnections = new Thread(HandleNewConnections);
            threadNewConnections.Start();

            messageMapper = new MessageMapper();
        }

        public void Stop()
        {
            tcpListener.Stop();

            threadNewConnections.Abort();

            var messageDisconnect = new Message {MessageType = MessageType.Disconnect};

            foreach (var listener in listeners.Values)
            {
                var clientStream = listener.TcpClient.GetStream();
                var streamWriter = new StreamWriter(clientStream);

                streamWriter.WriteLine(messageMapper.GetStringFromMessage(messageDisconnect));

                listener.Thread.Abort();
                listener.TcpClient.Close();
            }
        }

        private void HandleNewConnections()
        {
            while (true)
            {
                var tcpClient = tcpListener.AcceptTcpClient();
                madeConnections++;
                AddTcpClient(tcpClient);

                NotifyClientOfItsUsername(tcpClient, listeners.First(x => x.Value.TcpClient == tcpClient).Key);
                NotifyClientOfOtherConnectedClients(tcpClient);
                NotifyOtherClientsOfClientConnection(tcpClient);
            }
        }

        private void HandleMessages(TcpClient tcpClient)
        {
            while (true)
            {
                var stream = tcpClient.GetStream();
                var streamReader = new StreamReader(stream);
                var streamMessage = streamReader.ReadLine();
                var message = messageMapper.GetMessageFromString(streamMessage);

                InterpretMessage(message, tcpClient);

                if (!tcpClient.Connected)
                {
                    break;
                }
            }
        }

        private void InterpretMessage(Message message, TcpClient sender)
        {
            var messageType = message.MessageType;

            if (messageType == MessageType.NotRecognized)
            {
                return;
            }

            else if (messageType == MessageType.UsernameChanged)
            {
                OnChangeUsername(sender, message.SourceUsername, message.DestinationUsername);
            }

            else if (messageType == MessageType.Disconnect)
            {
                NotifyOtherClientsOfDisconnectedClient(sender);
                OnClientDisconnected(message.SourceUsername);

                var senderListener = listeners[message.SourceUsername];
                listeners.Remove(message.SourceUsername);
                senderListener?.Stop();
            }

            else if (messageType == MessageType.Chat)
            {
                var otherClients = listeners
                    .Values
                    .Select(x => x.TcpClient)
                    .Where(x => x != sender);

                foreach (var tcpClient in otherClients)
                {
                    SendMessageToClient(message, tcpClient);
                }
            }
        }

        private void SendMessageToClient(Message message, TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream);
            var messageString = messageMapper.GetStringFromMessage(message);

            streamWriter.WriteLine(messageString);
            streamWriter.Flush();
        }

        private void AddTcpClient(TcpClient tcpClient)
        {
            var username = $"user{madeConnections + 1}";
            var listener = new Listener
            {
                TcpClient = tcpClient,
                Thread = new Thread(() => HandleMessages(tcpClient))
            };

            listeners.Add(username, listener);
            listener.Thread.Start();

            OnClientConnected(username);
        }

        private void NotifyClientOfItsUsername(TcpClient tcpClient, string username)
        {
            var message = new Message
            {
                MessageType = MessageType.UsernameSent,
                Value = username
            };

            SendMessageToClient(message, tcpClient);
        }

        private void NotifyClientOfOtherConnectedClients(TcpClient tcpClient)
        {
            var otherClientsListeners = listeners
                .Values
                .Where(x => x.TcpClient != tcpClient);

            foreach (var otherClientListener in otherClientsListeners)
            {
                var message = new Message
                {
                    MessageType = MessageType.ClientConnected,
                    Value = listeners.First(x => x.Value == otherClientListener).Key
                };

                SendMessageToClient(message, tcpClient);
            }
        }

        private void NotifyOtherClientsOfClientConnection(TcpClient tcpClient)
        {
            var message = new Message
            {
                MessageType = MessageType.ClientConnected,
                Value = listeners.First(x => x.Value.TcpClient == tcpClient).Key
            };

            var otherClients = listeners
                .Values
                .Select(x => x.TcpClient)
                .Where(x => x != tcpClient);

            foreach (var otherTcpClient in otherClients)
            {
                SendMessageToClient(message, otherTcpClient);
            }
        }

        private void NotifyOtherClientsOfDisconnectedClient(TcpClient tcpClient)
        {
            var message = new Message
            {
                MessageType = MessageType.ClientDisconnected,
                Value = listeners.First(x => x.Value.TcpClient == tcpClient).Key
            };

            var otherClients = listeners
                .Values
                .Select(x => x.TcpClient)
                .Where(x => x != tcpClient);

            foreach (var otherTcpClient in otherClients)
            {
                SendMessageToClient(message, otherTcpClient);
            }
        }

        private void OnChangeUsername(TcpClient sender, string oldUsername, string newUsername)
        {
            var usernameAlreadyExists = listeners.Keys.Contains(newUsername);

            if (usernameAlreadyExists)
            {
                var message = new Message {MessageType = MessageType.NotOk, Value = $"Username '{newUsername}' is already used"};
                SendMessageToClient(message, sender);
            }
            else
            {
                var listener = listeners[oldUsername];
                listeners.Add(newUsername, listener);
                listeners.Remove(oldUsername);

                NotifyClientOfItsUsername(sender, newUsername);
                NotifyOtherClientsOfChangedUsername(sender, oldUsername, newUsername);
            }
        }

        private void NotifyOtherClientsOfChangedUsername(TcpClient sender, string oldUsername, string newUsername)
        {
            var otherClients = listeners
                .Values
                .Select(x => x.TcpClient)
                .Where(x => x != sender);

            var message = new Message
            {
                MessageType = MessageType.UsernameChanged,
                SourceUsername = oldUsername,
                DestinationUsername = newUsername
            };

            foreach (var otherClient in otherClients)
            {
                SendMessageToClient(message, otherClient);
            }
        }
    }
}