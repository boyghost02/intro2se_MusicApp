using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp
{
    [Serializable]
    public class Comment
    {
        private string user_;
        private string content_;

        public string User { get => user_; set => user_ = value; }
        public string Content { get => content_; set => content_ = value; }
        public Comment()
        {
            user_ = "";
            content_ = "";
        }
        public Comment(string user, string content)
        {
            this.user_ = user;
            this.content_ = content;
        }
    }
    [Serializable]
    public class Song
    {
        private string name_;
        private string singer_;
        private string url_;
        private int like_;
        private int playcount_;
        private string coverimage_;
        private List<Comment> comments_;
        private bool isRecent_;

        public string Name { get => name_; set => name_ = value; }
        public string Singer { get => singer_; set => singer_ = value; }
        public string Url { get => url_; set => url_ = value; }
        public int Like { get => like_; set => like_ = value; }
        public int Playcount { get => playcount_; set => playcount_ = value; }
        public List<Comment> Comments { get => comments_; set => comments_ = value; }
        public string CoverImage { get => coverimage_; set => coverimage_ = value; }
        public bool IsRecent { get => isRecent_; set => isRecent_ = value; }

        public Song()
        {
            name_ = "";
            singer_ = "";
            url_ = "";
            like_ = 0;
            playcount_ = 0;
            coverimage_ = "";
            comments_ = new List<Comment>();
            isRecent_ = false;
        }

        public Song(string name, string singer, string url, int like, int playcount, List<Comment> comments, string coverimage)
        {
            this.name_ = name;
            this.singer_ = singer;
            this.url_ = url;
            this.like_ = like;
            this.playcount_ = playcount;
            this.coverimage_ = coverimage;
            this.comments_ = comments;
            isRecent_ = false;
        }
    }
}
