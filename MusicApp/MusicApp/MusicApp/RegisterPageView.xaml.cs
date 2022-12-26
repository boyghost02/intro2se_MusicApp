using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private void Get_Register(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == txtPassword.Text)
            {
                
            }
            else
            {
                DisplayAlert("Ops..", "The confirm password is incorrect!", "OK");
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
    }
}