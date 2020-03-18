using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace APD.Networking
{
    public class Server
    {
        private TcpListener tcpListener;
        private readonly NetworkStream stream;
        private readonly StreamReader streamReader;
        private readonly StreamWriter streamWriter;
        private readonly List<TcpClient> tcpClients;

        public int Port { get; set; }

        public Server(TcpClient clientSocket)
        {
            stream = clientSocket.GetStream();
            streamReader = new StreamReader(stream);
            streamWriter = new StreamWriter(stream);

            tcpClients = new List<TcpClient>();

            var receivedString = ReceiveString();
            var destination = "Destination that I should get somehow";
            SendString(receivedString, destination);

            stream.Close();
        }

        public Server(int port)
        {
            // TODO: Check if port is already used
            Port = port;

            var thread = new Thread(() => Run(port));
            thread.Start();
        }

        public void SendString(string val, string destinationUsername)
        {
            streamWriter.WriteLine(val);
            streamWriter.Flush();
        }

        public string ReceiveString()
        {
            var receivedString = streamReader.ReadLine();
            Console.WriteLine(receivedString);

            return receivedString;
        }

        public void Stop()
        {

        }

        private void Run(int port)
        {
            tcpListener = new TcpListener(port);
            tcpListener.Start();

            while (true)
            {
                var clientSocket = tcpListener.AcceptTcpClient();
                new Server(clientSocket);
            }
        }
    }
}