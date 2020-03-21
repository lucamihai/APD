using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace APD.Networking
{
    public class Server
    {
        private readonly TcpListener tcpListener;
        private readonly ConcurrentDictionary<string, TcpClient> tcpClients;
        private readonly Thread threadListenMessages;
        private readonly Thread threadNewConnections;

        public int Port { get; }

        public Server(int port)
        {
            // TODO: Check if port is already used
            Port = port;

            tcpListener = new TcpListener(System.Net.IPAddress.Any, port);
            tcpListener.Start();

            tcpClients = new ConcurrentDictionary<string, TcpClient>();

            threadNewConnections = new Thread(HandleNewConnections);
            threadNewConnections.Start();
            threadListenMessages = new Thread(HandleMessages);
            threadListenMessages.Start();
        }

        public void Stop()
        {
            tcpListener.Stop();

            threadNewConnections.Abort();
            threadListenMessages.Abort();

            foreach (var tcpClient in tcpClients.Values)
            {
                tcpClient.Close();
            }
        }

        private void Run()
        {

        }

        private void HandleNewConnections()
        {
            while (true)
            {
                var tcpClient = tcpListener.AcceptTcpClientAsync();
                tcpClient.ContinueWith(t => AddTcpClient(t.Result));
            }
        }

        private void HandleMessages()
        {
            while (true)
            {
                foreach (var tcpClient in tcpClients.Values.Where(x => x.Connected))
                {
                    var stream = tcpClient.GetStream();
                    var streamReader = new StreamReader(stream);
                    var streamMessage = streamReader.ReadLine();

                    InterpretMessage(streamMessage);

                    stream.Close();
                }
            }
        }

        private void InterpretMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }


        }

        private void AddTcpClient(TcpClient tcpClient)
        {
            // TODO: Logic here to determine client username and add to tcpClients
            var username = $"user{tcpClients.Count + 1}";
            tcpClients.TryAdd(username, tcpClient);
        }
    }
}