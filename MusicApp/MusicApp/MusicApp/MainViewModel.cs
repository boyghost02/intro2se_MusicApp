using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MusicApp
{
    public class SearchItem
    {
        public string Name;
        public string Url;
    }

    public class SearchGroup: List<SearchItem>
    {
        public string Name { get; private set; }
        public SearchGroup(string name, List<SearchItem> searchItems):
            base(searchItems)
        {
            Name = name;
        }
    }

    public class MainViewModel
    {
        Song recentMusic;
        Account myAccount { get; set; } = new Account() { FirstName = "Huy", LastName = "Nguyen", Email = "20127038@student.hcmus.edu.vn" };
        ObservableCollection<Song> musicList;
        ObservableCollection<Comment> comments;

        public MainViewModel()
        {
            musicList = GetMusics();
            comments = GetComment();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Song> MusicListm
        {
            get { return musicList; }
            set { musicList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; OnPropertyChanged(); }
        }
        
        private ObservableCollection<Song> GetMusics()
        {
            return new ObservableCollection<Song>
            {
            };
        }
        private ObservableCollection<Comment> GetComment()
        {
            return new ObservableCollection<Comment>
            {
                new Comment { User="Son Nguyen", Content="good music"},
                new Comment {User = "Thach Lam", Content = "love it"}
            };
        }
    }
}
