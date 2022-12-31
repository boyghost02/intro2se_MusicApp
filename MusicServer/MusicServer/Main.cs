using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using MusicAppClass;

namespace MusicServer
{
    public partial class Main : Form
    {
        Listener listener;

        public Main()
        {
            InitializeComponent();
            listener = new Listener(8080);
            listener.SocketAccepted += new Listener.SocketAcceptedHandler(listener_SocketAccepted);
            Load += new EventHandler(Main_Load);
        }

        void Main_Load(object sender, EventArgs e)
        {
            listener.Start();
            if (listener.Listening)
            {
                Console.WriteLine($"Server start on {listener.Ip}:{listener.Port}");
            }
            else
            {
                Console.WriteLine("Can't start server");
            }
        }

        void listener_SocketAccepted(System.Net.Sockets.Socket e)
        {

            Console.WriteLine($"New connect from {e.RemoteEndPoint.ToString()}");

            Client client = new Client(e);
            client.Received += new Client.ClientReceiveHandler(client_Received);
            client.Disconnected += new Client.ClientDisconnectedHandler(client_Disconnected);

            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = client.EndPoint.ToString();
                i.SubItems.Add(client.ID);
                i.SubItems.Add("null");
                i.SubItems.Add("null");
                i.SubItems.Add("null");
                i.Tag = client;
                listClient.Items.Add(i);
            });
        }

        void client_Received(Client sender, byte[] data)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < listClient.Items.Count; i++)
                {
                    Client client = listClient.Items[i].Tag as Client;
                    if (client.ID == sender.ID)
                    {
                        string msg = "";
                        object obj = Deserialize(data);
                        if (obj is string)
                        {
                            client.Send(Serialize("NO"));
                            msg = (string)obj;
                        }
                        else
                        {

                            string request = listClient.Items[i].SubItems[2].Text;
                            switch (request)
                            {
                                case "Login":
                                    Account loginAccount = (Account)obj;
                                    if (loginAccount.Email.Contains("123"))
                                    {
                                        Console.WriteLine("Login");
                                        client.Send(Serialize("Login OK"));
                                        listClient.Items[i].SubItems[2].Text = null;
                                    }
                                    break;
                                case "Register":
                                    Account registerAccount = (Account)obj;
                                    if (true)
                                    {
                                        client.Send(Serialize("Register OK"));
                                    }
                                    else
                                    {
                                        client.Send(Serialize("Register Fail"));
                                    }
                                    break;
                            }
                        }
                        listClient.Items[i].SubItems[2].Text = msg;
                        listClient.Items[i].SubItems[3].Text = DateTime.Now.ToString();
                        break;
                    }
                }
            });
        }

        void client_Disconnected(Client sender)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < listClient.Items.Count; i++)
                {
                    Client client = listClient.Items[i].Tag as Client;
                    if (client.ID == sender.ID)
                    {
                        Console.WriteLine($"Disconnect from {client.EndPoint}");
                        listClient.Items.RemoveAt(i);
                        break;
                    }
                }
            });
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
