using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MusicApp
{
    public class MusicClient
    {
        IPAddress ip;
        IPEndPoint ep;
        public Socket socket;

        public MusicClient()
        {
            ip = IPAddress.Parse("192.168.8.1");
            ep = new IPEndPoint(ip, 8080);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        
        public void Connect()
        {
            try
            {
                socket.Connect(ep);
            }
            catch (Exception e)
            {
                return;
            }
            if (socket.Connected)
            {
                socket.Send(Serialize("Hello Server"));
            }
        }

        public void Disconnect()
        {
            socket.Close();
            socket.Dispose();
        }

        byte[] Serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);

            return stream.ToArray();
        }

        //gom mảnh dữ liệu đã phân mảnh
        object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter.Deserialize(stream);
        }
    }
}
