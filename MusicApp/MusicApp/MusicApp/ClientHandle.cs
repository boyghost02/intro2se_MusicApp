using MusicAppClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        bool islogin;   
        Account clientAccount;
        public ObservableCollection<Song> top20musics
        {
            get;
            private set;
        }
        public bool isLogin { get => islogin; set => islogin = value; }
        public Account ClientAccount { get => clientAccount; set => clientAccount = value; }
        
        public MusicClient()
        {
            ip = IPAddress.Parse("192.168.8.1");
            ep = new IPEndPoint(ip, 8080);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            islogin = false;
            top20musics = new ObservableCollection<Song> { new Song { IsRecent = true } };
            clientAccount = new Account { FirstName = "null", LastName="null", Email="null"};
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
                    string json = (string)Deserialize(data);
                    top20musics = JsonConvert.DeserializeObject<ObservableCollection<Song>>(json);
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
