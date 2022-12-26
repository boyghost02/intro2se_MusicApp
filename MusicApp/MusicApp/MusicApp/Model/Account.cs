using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MusicApp
{
    [Serializable]
    public enum TypeOfAccount
    {
        NormalUser,
        VipUser,
        Admin,
        Artist,
        Banned
    }

    [Serializable]
    public class Account
    {
        private string password_;
        private TypeOfAccount type_;
        private string email_;
        private ObservableCollection<Song> playlist_;
        private string firstname_;
        private string lastname_;

        public string Password { get => password_; set => password_ = value; }
        public TypeOfAccount Type { get => type_; set => type_ = value; }
        public string Email { get => email_; set => email_ = value; }
        internal ObservableCollection<Song> Playlist { get => playlist_; set => playlist_ = value; }
        public string FirstName { get => firstname_; set => firstname_ = value; }
        public string LastName { get => lastname_; set => lastname_ = value; }

        public Account()
        {
            password_ = "";
            type_ = TypeOfAccount.Banned;
            email_ = "";
            playlist_ = new ObservableCollection<Song>();
            firstname_ = "";
            lastname_ = "";
        }

        public Account(string email, string password, TypeOfAccount type, ObservableCollection<Song> playlist, string firstname, string lastname)
        {
            password_ = password;
            type_ = type;
            email_ = email;
            playlist_ = playlist;
            firstname_ = firstname;
            lastname_ = lastname;
        }
    }

    [Serializable]
    public class ListAccount
    {
        private static ListAccount instance_;
        List<Account> listAccount_;

        public static ListAccount Instance
        {
            get
            {
                if (instance_ == null)
                {
                    instance_ = new ListAccount();
                }
                return instance_;
            }
            set => instance_ = value;
        }

        public List<Account> listAccount { get => listAccount_; set => listAccount_ = value; }

        private ListAccount()
        {
            listAccount_ = new List<Account>();
        }
    }
}