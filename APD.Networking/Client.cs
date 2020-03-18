using System;
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
            stream = tcpClient.GetStream(); streamReader = new StreamReader(stream);
            streamWriter = new StreamWriter(stream); 
            
            SendString("Merge!"); 
            stream.Close(); 
            tcpClient.Close();
        }

        public void SendInt(int val)
        {
            streamWriter.WriteLine(Convert.ToString(val)); 
            streamWriter.Flush();
        }

        public void SendString(string message)
        {
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }

        public int ReceiveInt()
        {
            var receivedInt = Convert.ToInt32(streamReader.ReadLine()); 
            Console.WriteLine(receivedInt);

            return receivedInt;
        }
    }
}