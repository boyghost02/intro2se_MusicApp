using MusicAppClass;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPageView : ContentPage
    {
        public LoginPageView()
        {
            InitializeComponent();

        }
        private void Get_Back(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlaylistPageView());
        }

        private bool check()
        {
            if (txtEmail.Text == null || txtPassword.Text == null)
            {
                DisplayAlert("Ops..", "Please fill in all the information!", "OK");
                return false;
            }
            try
            {
                MailAddress m = new MailAddress(txtEmail.Text);
                return true;
            }
            catch (FormatException)
            {
                DisplayAlert("Ops..", "Please enter a valid email address!", "OK");
                return false;
            }
            return true;
        }

        private void Get_Login(object sender, EventArgs e)
        {
            if (App.client.isLogin == true)
            {
                DisplayAlert("Ops..", "You are login!", "OK");
            }
            else
            {
                if (check() == false)
                {

                }
                else
                {
                    byte[] data = new byte[8192];
                    App.client.socket.Send(Serialize("Login"));
                    App.client.socket.Receive(data);
                    Account loginAccount = new Account(txtEmail.Text, txtPassword.Text, TypeOfAccount.NormalUser, null, null, null);
                    App.client.socket.Send(Serialize(loginAccount));
                    Task.Delay(500);
                    App.client.socket.Receive(data);
                    string s = (string)Deserialize(data);
                    if (s.Contains("Login Fail") || s.Contains("TEMP MSG"))
                    {
                        DisplayAlert("Ops..", "Username or Password is incorrect!", "OK");
                    }
                    else
                    {
                        Account clientAccount = JsonConvert.DeserializeObject<Account>(s);
                        if (clientAccount.Type == TypeOfAccount.Banned)
                        {
                            DisplayAlert("Ops..", "Your account is banned!", "OK");
                        }
                        else
                        {
                            DisplayAlert("Success..", "Login successful!", "OK");
                            App.client.ClientAccount = clientAccount;
                            App.client.isLogin = true;
                            OnPropertyChanged();
                            Navigation.PushAsync(new PlaylistPageView());
                        }
                    }
                }
            }
        }


        private void Move_To_RegisterPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPageView());
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