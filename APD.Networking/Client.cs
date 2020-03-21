using System.IO;
using System.Net.Sockets;

namespace APD.Networking
{
    public class Client
    {
        private readonly TcpClient tcpClient;
        private readonly NetworkStream stream;
        private readonly StreamReader streamReader;
        private readonly StreamWriter streamWriter;

        public Client(string hostname, int port)
        {
            tcpClient = new TcpClient(hostname, port);
            stream = tcpClient.GetStream();

            streamReader = new StreamReader(stream);
            streamWriter = new StreamWriter(stream);
        }

        public void Stop()
        {
            stream.Close();
            tcpClient.Close();
        }

        public void SendString(string message)
        {
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }
    }
}