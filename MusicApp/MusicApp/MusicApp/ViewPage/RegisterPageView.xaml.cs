using MusicAppClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPageView : ContentPage
	{
		public RegisterPageView ()
		{
			InitializeComponent ();
		}
        
        private bool check()
        {
            if (txtEmail == null || txtPassword == null || txtConfirmPassword == null || txtFirstName == null || txtLastName == null) 
            {
                return false;
            }
            return true;
        }
        
        private void Get_Register(object sender, EventArgs e)
        {
            if (check() == false)
            {
                DisplayAlert("Ops..", "Please fill in all the information!", "OK");
            }
            {
                byte[] data = new byte[8192];
                App.client.socket.Send(Serialize("Register"));
                App.client.socket.Receive(data);

                Account loginAccount = new Account(txtEmail.Text, txtPassword.Text, TypeOfAccount.NormalUser, null, txtFirstName.Text, txtLastName.Text);
                App.client.socket.Send(Serialize(loginAccount));
                App.client.socket.Receive(data);
                string s = (string)Deserialize(data);
                if (s.Contains("Register OK"))
                {
                    DisplayAlert("Success", "Register Successful", "OK");
                }
                else if (s.Contains("Register Fail"))
                {
                    DisplayAlert("Ops..", "Account already exists!", "OK");
                }
            }
        }

        private void Move_To_Login_Page(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPageView());
        }

        

        private void Get_Back(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePageView());
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