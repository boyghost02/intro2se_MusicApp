using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MusicAppClass;
using Newtonsoft.Json;

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
                string json = JsonConvert.SerializeObject(GetTop20Music());
                client.Send(Serialize(json));
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
                        object obj = null;
                        try
                        {
                            obj = Deserialize(data);
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                            client_Disconnected(client);
                            return;
                        }
                        if (obj is string)
                        {
                            msg = (string)obj;
                            if (msg.Contains("Search"))
                            {
                                string search = msg.Split('|')[1];
                                search = search.Trim();
                                Regex.Replace(search, @"\s+", " ");
                                search = LocDau(search);
                                string json = JsonConvert.SerializeObject(GetTop20Music());
                                client.Send(Serialize(json));
                            }
                            else
                            {
                                client.Send(Serialize("TEMP MSG"));
                            }
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
                                        ObservableCollection<Song> songs = accountSong();

                                        Account account = new Account("123@gmail.com", "", TypeOfAccount.NormalUser, songs, "Nguyen Van", "A");
                                        string json = JsonConvert.SerializeObject(account);
                                        client.Send(Serialize(json));
                                        listClient.Items[i].SubItems[2].Text = null;
                                    }
                                    else
                                    {
                                        client.Send(Serialize("Login Fail"));
                                        listClient.Items[i].SubItems[2].Text = null;
                                    }
                                    break;
                                case "Register":
                                    Account registerAccount = (Account)obj;
                                    if (true)
                                    {
                                        client.Send(Serialize("Register OK"));
                                        listClient.Items[i].SubItems[2].Text = null;
                                    }
                                    else
                                    {
                                        client.Send(Serialize("Register Fail"));
                                        listClient.Items[i].SubItems[2].Text = null;
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
        ObservableCollection<Song> accountSong()
        {
            return new ObservableCollection<Song>
            {
                new Song
                {
                    Name = "Đố anh đoán được 11", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg", IsRecent = true
                },
                new Song
                {
                    Name = "Đố anh đoán được 22", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 33", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                }
            };
        }

        private ObservableCollection<Song> GetTop20Music()
        {
            return new ObservableCollection<Song>
            {
                new Song
                {
                    Name = "Đố anh đoán được 1", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg", Like=100, IsRecent = true
                },
                new Song
                {
                    Name = "Đố anh đoán được 2", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 3", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 4", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 5", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 6", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 7", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 8", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 9", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được 10", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                }
            };
        }

        private static readonly string[] VietNamChar = new string[]
    {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
    };
        public static string LocDau(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
    }
}
