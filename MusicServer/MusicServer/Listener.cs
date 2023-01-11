using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MusicServer
{
    public class Listener
    {
        Socket s;
        private bool listening;
        private int port;
        private IPAddress ip;

        public bool Listening { get => listening; set => listening = value; }
        public int Port { get => port; set => port = value; }
        public IPAddress Ip { get => ip; set => ip = value; }

        public Listener(int port)
        {
            this.port = port;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ip = IPAddress.Parse("192.168.8.1");
        }
        public void Start()
        {
            if (listening)
                return;
            s.Bind(new IPEndPoint(Ip, Port));
            s.Listen(1000);
            s.BeginAccept(callback, null);
            listening = true;
        }
        
        public void Stop()
        {
            if (!listening)
                return;
            s.Close();
            s.Dispose();
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
        }

        void callback(IAsyncResult ar)
        {
            try
            {
                Socket socket = this.s.EndAccept(ar);

                if (SocketAccepted != null)
                {
                    SocketAccepted(socket);
                }

                this.s.BeginAccept(callback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;
    }
}