using System.Net.Sockets;
using System.Threading;

namespace APD.Networking.Utilities
{
    public class Listener
    {
        public TcpClient TcpClient { get; set; }
        public Thread Thread { get; set; }

        public void Stop()
        {
            Thread.Abort();
            TcpClient.Close();
        }
    }
}