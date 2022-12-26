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
	public partial class HomePageView : ContentPage
	{
		public HomePageView ()
		{
			InitializeComponent ();
		}

        private void Search_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPageView());
        }
    }
}