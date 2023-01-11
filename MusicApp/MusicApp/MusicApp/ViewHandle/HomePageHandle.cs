using MusicAppClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp
{
    public class HomePageHandle : BaseViewModel
    {
        public HomePageHandle()
        {
            musicList = GetMusics();
        }

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

        private ObservableCollection<Song> GetMusics()
        {
            return App.client.top20musics;
        }
    }
}
