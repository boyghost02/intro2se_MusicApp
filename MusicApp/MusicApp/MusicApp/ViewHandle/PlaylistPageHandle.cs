using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MusicAppClass;

namespace MusicApp
{
    public class PlaylistPageHandle : BaseViewModel
    {
        public PlaylistPageHandle()
        {
            musicList = GetMusics();
            recentMusic = musicList.Where(x => x.IsRecent == true).FirstOrDefault();
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

        private Song recentMusic;
        public Song RecentMusic
        {
            get { return recentMusic; }
            set
            {
                recentMusic = value;
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
            return new ObservableCollection<Song>
            {
                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg", IsRecent = true
                },
                                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                                                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                                                                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                                                                                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                },
                                                                                                new Song
                {
                    Name = "Đố anh đoán được", Singer = "Bích Phương", Url="http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3", CoverImage="http://192.168.8.1:8082/music/DoAnhDoanDuoc.jpg"
                }
            };
        }
    }
}
