using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
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
                                search = FormatString(search);
                                ObservableCollection<Song> searchRes = new ObservableCollection<Song>();
                                bool found = false;
                                foreach (Song song in MusicList())
                                {
                                    string str1 = song.Url.ToUpper();
                                    string str2 = search.ToUpper();
                                    if (str1.Contains(str2))
                                    {
                                        searchRes.Add(song);
                                        found = true;
                                    }
                                }
                                if (found)
                                { }
                                else
                                    searchRes = null;
                                string json = JsonConvert.SerializeObject(searchRes);
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
                                    if (checkLogin(loginAccount))
                                    {
                                        ServerData accountData = new ServerData();
                                        ObservableCollection<Account> accounts = accountData.AccountList();
                                        Account account = accounts.Where(x => x.Email == loginAccount.Email).FirstOrDefault();
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
                                    if (checkRegister(registerAccount))
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

        private ObservableCollection<Song> MusicList()
        {
            ServerData songData = new ServerData();
            ObservableCollection<Song> res = songData.MusicList();
            return res;
        }


        private ObservableCollection<Song> GetTop20Music()
        {
            ObservableCollection<Song> top20Songs = new ObservableCollection<Song>(MusicList().OrderByDescending(s => s.Like).Take(20));
            return top20Songs;
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
        public string FormatString(string str)
        {
            string res = str.Trim();
            res = Regex.Replace(res, @"\s+", "");
            res = LocDau(res);
            return res;
        }
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

        public bool checkLogin(Account account)
        {
            ServerData accountData = new ServerData();
            ObservableCollection<Account> accounts = accountData.AccountList();
            foreach (Account acc in accounts)
            {
                if (account.Email == acc.Email && account.Password == acc.Password)
                {
                    return true;
                }
            }
            return false;
        }
        
        public bool checkRegister(Account account)
        {
            ServerData accountData = new ServerData();
            ObservableCollection<Account> accounts = accountData.AccountList();
            foreach (Account acc in accounts)
            {
                if (account.Email == acc.Email)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
