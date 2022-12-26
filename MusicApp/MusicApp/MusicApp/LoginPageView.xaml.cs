using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPageView : ContentPage
	{
		public LoginPageView ()
		{
			InitializeComponent ();
            
        }
        private void Get_Back(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePageView());
        }

        private void Get_Login(object sender, EventArgs e)
        {
            if(txtEmail.Text=="admin@gmail.com" && txtPassword.Text == "123")
            {

            }
            else
            {
                DisplayAlert("Ops..", "Username or Password is incorrect!", "OK");
            }
        }


        private void Move_To_RegisterPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPageView());
        }

    }

}