using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using APD.Networking.Entities;
using APD.Networking.Utilities;

namespace APD.Networking
{
    public class Client
    {
        private readonly TcpClient tcpClient;
        private readonly NetworkStream stream;
        private readonly StreamReader streamReader;
        private readonly StreamWriter streamWriter;
        private readonly MessageMapper messageMapper;
        private readonly Thread threadListen;
        public string username;

        public List<string> OtherClients
        {
            get
            {
                var list = new List<string>();
                list.AddRange(otherClients);

                return list;
            }
        }
        private readonly List<string> otherClients;


        public delegate void ChatReceived(string chatMessage, string sourceUsername, string destinationUsername);
        public ChatReceived OnChatReceived { get; set; } = delegate(string message, string sourceUsername, string destinationUsername) {  };

        public delegate void OtherClientConnected(string username);
        public OtherClientConnected OnOtherClientConnected { get; set; } = delegate(string s) {  };

        public delegate void OtherClientDisconnected(string username);
        public OtherClientDisconnected OnOtherClientDisconnected { get; set; } = delegate(string s) {  };

        public Client(string hostname, int port)
        {
            tcpClient = new TcpClient(hostname, port);
            stream = tcpClient.GetStream();

            streamReader = new StreamReader(stream);
            streamWriter = new StreamWriter(stream);

            messageMapper = new MessageMapper();

            username = "some username";

            threadListen = new Thread(Listen);
            threadListen.Start();

            otherClients = new List<string>();
        }

        public void Stop()
        {
            stream.Close();
            tcpClient.Close();
        }

        public void SendChat(string str, string destinationUserName)
        {
            var message = new Message
            {
                MessageType = MessageType.Chat,
                Value = str,
                SourceUsername = username,
                DestinationUsername = destinationUserName,
            };

            var messageAsString = messageMapper.GetStringFromMessage(message);
            streamWriter.WriteLine(messageAsString);
            streamWriter.Flush();
        }
 
        public void Listen()
        {
            while (true)
            {
                var stream = tcpClient.GetStream();
                var streamReader = new StreamReader(stream);
                var streamMessage = streamReader.ReadLine();
                var message = messageMapper.GetMessageFromString(streamMessage);

                InterpretMessage(message);
            }
        }

        private void InterpretMessage(Message message)
        {
            var messageType = message.MessageType;

            if (messageType == MessageType.Chat)
            {
                OnChatReceived(message.Value, message.SourceUsername, message.DestinationUsername);
            }

            if (messageType == MessageType.ClientConnected)
            {
                var otherClientUsername = message.Value;
                otherClients.Add(otherClientUsername);
                OnOtherClientConnected(otherClientUsername);
            }

            if (messageType == MessageType.ClientDisconnected)
            {
                var otherClientUsername = message.Value;
                otherClients.Remove(otherClientUsername);
                OnOtherClientDisconnected(otherClientUsername);
            }
        }
    }
}