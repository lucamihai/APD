using System.IO;
using System.Net.Sockets;
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
        private string username;

        public Client(string hostname, int port)
        {
            tcpClient = new TcpClient(hostname, port);
            stream = tcpClient.GetStream();

            streamReader = new StreamReader(stream);
            streamWriter = new StreamWriter(stream);

            messageMapper = new MessageMapper();

            username = "some username";
        }

        public void Stop()
        {
            stream.Close();
            tcpClient.Close();
        }

        public void SendString(string str)
        {
            // TODO: Determine destination
            var destination = "insert destination here";

            var message = new Message
            {
                MessageType = MessageType.Chat,
                Value = str,
                SourceUsername = username,
                DestinationUsername = destination,
            };
            var messageAsString = messageMapper.GetStringFromMessage(message);

            //streamWriter.WriteLine(str);
            streamWriter.WriteLine(messageAsString);
            streamWriter.Flush();
        }
    }
}