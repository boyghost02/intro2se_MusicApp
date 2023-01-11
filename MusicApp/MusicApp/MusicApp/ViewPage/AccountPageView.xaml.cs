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

	public partial class AccountPageView : ContentPage
	{
        
        public AccountPageView ()
		{
            InitializeComponent();
        }
        private void Get_Back(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DashboardPageView());
        }

        private void Move_To_Playlist(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlaylistPageView());                      
        }

        private void Move_To_DownloadPlaylist(object sender, EventArgs e)
        {
            
        }

        private void Move_To_UploadPage(object sender, EventArgs e)
        {
            
        }

        private void Move_To_ManagePage(object sender, EventArgs e)
        {

        }
        private void Get_Logout(object sender, EventArgs e)
        {

        }
    }
}