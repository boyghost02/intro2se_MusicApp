using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MusicAppClass;

namespace MusicApp
{
    public class CommentPageHandle
    {
        public CommentPageHandle(List<Comment> ListComments)
        {
            comments = ListComments;
        }

        private List<Comment> comments;

        public ICommand BackCommand => new Command(() => Application.Current.MainPage.Navigation.PopAsync());

        public List<Comment> Comments { get => comments; set => comments = value; }
    }
}
