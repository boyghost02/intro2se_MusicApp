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
            client.Connect();
            MainPage = new NavigationPage(new DashboardPageView());
        }

        protected override void OnStart()
        {
            if (!client.socket.Connected)
                client.Connect();
        }

        protected override void OnSleep()
        {
            if (client.socket.Connected)
                client.Disconnect();
        }

        protected override void OnResume()
        {
            if (!client.socket.Connected)
                client.Connect();
        }        
    }
}