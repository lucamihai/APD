using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using APD.Networking.Entities;
using APD.Networking.Utilities;

namespace APD.Networking
{
    public class Server
    {
        private readonly TcpListener tcpListener;
        private readonly Dictionary<string, Listener> listeners;
        private readonly Thread threadNewConnections;
        private readonly MessageMapper messageMapper;

        public int Port { get; }

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

            foreach (var listener in listeners.Values)
            {
                listener.Thread.Abort();
                listener.TcpClient.Close();
            }
        }

        private void HandleNewConnections()
        {
            while (true)
            {
                var tcpClient = tcpListener.AcceptTcpClient();
                AddTcpClient(tcpClient);
            }
        }

        private void HandleMessages(TcpClient tcpClient)
        {
            while (true)
            {
                var stream = tcpClient.GetStream();
                var streamReader = new StreamReader(stream);
                var streamMessage = streamReader.ReadLine();

                InterpretMessage(streamMessage);

                if (!tcpClient.Connected)
                {
                    break;
                }
            }
        }

        private void InterpretMessage(string stringMessage)
        {
            if (string.IsNullOrEmpty(stringMessage))
            {
                return;
            }

            var message = messageMapper.GetMessageFromString(stringMessage);

            if (message.MessageType == MessageType.NotRecognized)
            {
                return;
            }

            if (message.MessageType == MessageType.Disconnect)
            {
                var senderListener = listeners[message.SourceUsername];
                senderListener?.Stop();
            }

            if (message.MessageType == MessageType.Chat)
            {

            }
        }

        private void AddTcpClient(TcpClient tcpClient)
        {
            // TODO: Logic here to determine client username and add to tcpClients
            var username = $"user{listeners.Count + 1}";

            if (listeners.ContainsKey(username))
            {
                // TODO: Prompt for new username
            }
            else
            {
                var listener = new Listener
                {
                    TcpClient = tcpClient,
                    Thread = new Thread(() => HandleMessages(tcpClient))
                };

                listeners.Add(username, listener);
                listener.Thread.Start();
            }
        }
    }
}