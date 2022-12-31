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
    public partial class SearchPageView : ContentPage
    {
        public List<SearchGroup> SearchItems { get; set; } = new List<SearchGroup>();
        public SearchPageView()
        {
            InitializeComponent();
            SearchItems.Add(new SearchGroup("Artists", new List<SearchItem>
            {
                new SearchItem{ Name="Bích Phương",Url=""},
                new SearchItem{ Name="Sơn Tùng",Url=""},
            }));
            SearchItems.Add(new SearchGroup("Songs", new List<SearchItem>
            {
                new SearchItem{ Name="I'll Follow You",Url=""},
                new SearchItem{ Name="Đố Anh Đoán Được",Url=""},
                new SearchItem{ Name="I'll Follow You",Url=""},
                new SearchItem{ Name="Đố Anh Đoán Được",Url=""},
                new SearchItem{ Name="I'll Follow You",Url=""},
                new SearchItem{ Name="Đố Anh Đoán Được",Url=""},
                new SearchItem{ Name="I'll Follow You",Url=""},
                new SearchItem{ Name="Đố Anh Đoán Được",Url=""},
                new SearchItem{ Name="I'll Follow You",Url=""},
                new SearchItem{ Name="Đố Anh Đoán Được",Url=""},
                new SearchItem{ Name="I'll Follow You",Url=""},
                new SearchItem{ Name="Đố Anh Đoán Được",Url=""},
            }));
            BindingContext = this;
        }
        
        
        private void Get_Back(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePageView());
        }

    }
}