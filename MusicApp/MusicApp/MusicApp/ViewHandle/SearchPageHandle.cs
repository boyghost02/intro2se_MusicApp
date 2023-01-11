using MusicAppClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp
{
    public class SearchPageHandle : BaseViewModel
    {
        public SearchPageHandle(string searchStr, ObservableCollection<Song> SearchSong)
        {
            searchText = searchStr;
            MusicList = SearchSong;
        }

        string searchText;
        public string SearchText2 { get { return "Search Results of " + searchText + " : "; } set { searchText = value; OnPropertyChanged(); } }
        public string SearchText { get { return searchText; } set { searchText = value; OnPropertyChanged(); } }
        ObservableCollection<Song> musicList;
        public ObservableCollection<Song> MusicList
        {
            get { return musicList; }
            set
            {
                musicList = value;
                OnPropertyChanged();
            }
        }

        private Song selectedMusic;
        public Song SelectedMusic
        {
            get { return selectedMusic; }
            set
            {
                selectedMusic = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(PlayMusic);
        private void PlayMusic()
        {
            if (selectedMusic != null)
            {
                var viewModel = new PlayerPageHandle(selectedMusic, musicList);
                var playerPage = new PlayerPageView { BindingContext = viewModel };

                var navigation = Application.Current.MainPage as NavigationPage;
                navigation.PushAsync(playerPage, true);
            }
        }
    }
}
