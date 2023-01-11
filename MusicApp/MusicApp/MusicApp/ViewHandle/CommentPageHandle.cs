using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MusicAppClass;

namespace MusicApp
{
    public class CommentPageHandle : BaseViewModel
    {
        public CommentPageHandle(List<Comment> comment)
        {
            comments = comment;
        }

        List<Comment> comments;
        public List<Comment> Comments { get { return comments; } set { comments = value; OnPropertyChanged(); } }
    }
}
