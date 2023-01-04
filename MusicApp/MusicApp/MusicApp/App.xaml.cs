using System;
using System.Net.Sockets;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicApp
{
    public partial class App : Application
    {
        public static MusicClient client;
        public App()
        {
            InitializeComponent();
            client = new MusicClient();
            MainPage = new NavigationPage(new DashboardPageView());
        }

        protected override void OnStart()
        {
            client.Connect();
        }

        protected override void OnSleep()
        {
            client.Disconnect();
        }

        protected override void OnResume()
        {
            client.Connect();
        }        
    }
}