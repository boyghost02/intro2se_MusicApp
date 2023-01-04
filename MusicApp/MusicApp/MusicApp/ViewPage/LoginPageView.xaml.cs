using MusicAppClass;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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
            Navigation.PushAsync(new HomePageView());
        }

        private bool check()
        {
            if (txtEmail.Text == null || txtPassword.Text == null)
            {
                return false;
            }
            return true;
        }

        private void Get_Login(object sender, EventArgs e)
        {
            if (check() == false)
            {
                DisplayAlert("Ops..", "Please fill in all the information!", "OK");
            }
            else
            {
                byte[] data = new byte[8192];
                App.client.socket.Send(Serialize("Login"));
                App.client.socket.Receive(data);

                Account loginAccount = new Account(txtEmail.Text, txtPassword.Text, TypeOfAccount.NormalUser, null, null, null);
                App.client.socket.Send(Serialize(loginAccount));
                App.client.socket.Receive(data);
                string s = (string)Deserialize(data);
                if (s.Contains("Login OK"))
                {
                    DisplayAlert("Success", "Login Successful", "OK");
                }
                else
                {
                    DisplayAlert("Ops..", "Username or Password is incorrect!", "OK");
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