using MediaManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using MusicAppClass;

namespace MusicApp
{
    public class PlayerPageHandle : BaseViewModel
    {
        public PlayerPageHandle(Song selectedMusic, ObservableCollection<Song> musicList)
        {
            this.selectedMusic = selectedMusic;
            this.musicList = musicList;
            PlayMusic(selectedMusic);
            isPlaying = true;
        }
        
        #region Properties
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

        private TimeSpan duration;
        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan position;
        public TimeSpan Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        double maximum = 100f;
        public double Maximum
        {
            get { return maximum; }
            set
            {
                if (value > 0)
                {
                    maximum = value;
                    OnPropertyChanged(); 
                }
            }
        }


        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlayIcon));
            }
        }

        public string PlayIcon { get => isPlaying ? "pause.png" : "play.png"; }

        #endregion

        public ICommand PlayCommand => new Command(Play);
        public ICommand ChangeCommand => new Command(ChangeMusic);
        public ICommand BackCommand => new Command(() => Application.Current.MainPage.Navigation.PopAsync());
        public ICommand ShareCommand => new Command(() => Share.RequestAsync(selectedMusic.Url, selectedMusic.Name));

        public ICommand AddPlaylistCommand => new Command(AddPlaylist);
        public ICommand LoveCommand => new Command(LoveSong);
        public ICommand DownloadCommand => new Command(DownloadSong);
        public ICommand CommentCommand => new Command(CommentSong);

        private void AddPlaylist()
        {
            if (App.client.isLogin == true)
            {
                App.client.ClientAccount.Playlist.Add(selectedMusic);
                OnPropertyChanged();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", "You need to login to use this feature", "OK");
            }
        }

        private void LoveSong()
        {
            if (App.client.isLogin == true)
            {
                selectedMusic.Like += 1;
                var currentIndex = musicList.IndexOf(selectedMusic);
                musicList[currentIndex].Like += 1;
                OnPropertyChanged();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", "You need to login to use this feature", "OK");
            }
        }

        private void CommentSong()
        {
            if (App.client.isLogin == true)
            {
                var viewModel = new CommentPageHandle(selectedMusic.Comments);
                var commentPage = new CommentPageView { BindingContext = viewModel };

                var navigation = Application.Current.MainPage as NavigationPage;
                navigation.PushAsync(commentPage, true);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", "You need to login to use this feature", "OK");
            }
        }

        private void DownloadSong()
        {

        }

        private async void Play()
        {
            if(isPlaying)
            {
                await CrossMediaManager.Current.Pause();
                IsPlaying = false; ;
            }
            else
            {
                await CrossMediaManager.Current.Play();
                IsPlaying = true; ;
            }
        }
        
        private void ChangeMusic(object obj)
        {
            if ((string)obj == "P")
                PreviousMusic();
            else if ((string)obj == "N")
                NextMusic();
        }

        private async void PlayMusic(Song music)
        {
            var mediaInfo = CrossMediaManager.Current;
            await mediaInfo.Play(music?.Url);
            IsPlaying = true;

            mediaInfo.MediaItemFinished += (sender, args) =>
            {
                IsPlaying = false;
                NextMusic();
            };

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () => 
            {
                Duration = mediaInfo.Duration;
                Maximum = duration.TotalSeconds;
                Position = mediaInfo.Position;
                return true;
            });
        }
        
        private void NextMusic()
        {
            var currentIndex = musicList.IndexOf(selectedMusic);
            if (currentIndex < musicList.Count - 1)
            {
                CrossMediaManager.Current.Stop();
                SelectedMusic = musicList[currentIndex + 1];
                PlayMusic(selectedMusic);
            }
        }

        private void PreviousMusic()
        {
            var currentIndex = musicList.IndexOf(selectedMusic);

            if (currentIndex > 0)
            {
                CrossMediaManager.Current.Stop();
                SelectedMusic = musicList[currentIndex - 1];
                PlayMusic(selectedMusic);
            }
        }
    }
}
