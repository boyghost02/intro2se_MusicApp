using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using MusicAppClass;

namespace MusicApp
{
    public class MusicClient
    {
        IPAddress ip;
        IPEndPoint ep;
        public Socket socket;
        public ObservableCollection<Song> top20Song { get; private set; } 

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
                byte[] data = new byte[8192];
                socket.Receive(data);
                try
                {
                    top20Song = (ObservableCollection<Song>)Deserialize(data);
                }
                catch (Exception e)
                {
                    
                }
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
