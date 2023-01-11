using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MusicApp
{
    public class AccountPageHandle
    {
        public string FirstName, LastName, Email;

        public AccountPageHandle()
        {
            FirstName = App.client.ClientAccount.FirstName;
            LastName = App.client.ClientAccount.LastName;
            Email = App.client.ClientAccount.Email;
        }
    }
}