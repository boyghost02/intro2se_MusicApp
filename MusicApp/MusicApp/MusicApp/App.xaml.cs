using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicApp
{
    public partial class App : Application
    {
        public App()
        {
<<<<<<< Updated upstream
            InitializeComponent();

            MainPage = new NavigationPage(new AccountPageView());
=======
            client = new MusicClient();
            client.Connect();
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPageView());
>>>>>>> Stashed changes
        }

        protected override void OnStart()
        {
<<<<<<< Updated upstream
=======
            if (!client.socket.Connected)
            client.Connect();
>>>>>>> Stashed changes
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
