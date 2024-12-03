using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Hacker_Simulator
{
    public class NetworkManager
    {
        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;
        private Thread listenThread;
        private bool isServer;

        public event Action<string> OnMessageReceived;

        public void StartServer(int port)
        {
            isServer = true;
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            listenThread = new Thread(ListenForClients);
            listenThread.Start();
        }

        public void ConnectToServer(string ipAddress, int port)
        {
            isServer = false;
            client = new TcpClient(ipAddress, port);
            stream = client.GetStream();
            listenThread = new Thread(ListenForMessages);
            listenThread.Start();
        }

        private void ListenForClients()
        {
            while (true)
            {
                var client = server.AcceptTcpClient();
                stream = client.GetStream();
                ListenForMessages();
            }
        }

        private void ListenForMessages()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    OnMessageReceived?.Invoke(message);
                }
            }
        }

        public void SendMessage(string message)
        {
            if (stream != null)
            {
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        public void Stop()
        {
            stream?.Close();
            client?.Close();
            server?.Stop();
            listenThread?.Abort();
        }
    }
}
