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
    public partial class CommentPageView : ContentPage
    {
        public CommentPageView()
        {
            InitializeComponent();
        }

        private void Send_Comment(object sender, EventArgs e)
        {
            //Send(txtComment)
        }

        //private void Get_Back(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new HomePageView());
        //}
    }
}