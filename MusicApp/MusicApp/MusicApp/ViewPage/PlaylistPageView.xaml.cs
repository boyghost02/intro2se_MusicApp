using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MusicApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PlaylistPageView : ContentPage
    {
        public PlaylistPageView()
        {
            InitializeComponent();

            if (App.client.isLogin == true)
            {
            }
            else
            {
                Navigation.PushAsync(new LoginPageView(), true);
            }
        }
    }
}
