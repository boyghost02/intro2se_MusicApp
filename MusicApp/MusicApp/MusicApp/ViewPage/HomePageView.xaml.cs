using MusicAppClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
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
        
		private void SearchCompleted(object sender, EventArgs e)
		{
            string search = txtSearch.Text;
            if (search != null)
            {
                string pattern = @"[!@#%^&*()_+=]";
                if (Regex.IsMatch(search, pattern))
                    DisplayAlert("Error", "Can not search with special symbols.", "OK");
                else
                {
                    string searchStr = "Search|" + search;
                    App.client.socket.Send(Serialize(searchStr));
                    Task.Delay(1000);
                    byte[] data = new byte[8192];
                    App.client.socket.Receive(data);
                    string json = (string)Deserialize(data);
                    ObservableCollection<Song> searchSong = JsonConvert.DeserializeObject<ObservableCollection<Song>>(json);
                    var viewModel = new SearchPageHandle(search, searchSong);
                    var searchPage = new SearchPageView { BindingContext = viewModel };
                    var navigation = Application.Current.MainPage as NavigationPage;
                    navigation.PushAsync(searchPage, true);
                    OnPropertyChanged();
                    Navigation.PushAsync(new SearchPageView());
                }
            }
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