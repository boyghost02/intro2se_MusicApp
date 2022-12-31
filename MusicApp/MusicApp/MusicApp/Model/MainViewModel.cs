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

    public class SearchGroup : List<SearchItem>
    {
        public string Name { get; private set; }
        public SearchGroup(string name, List<SearchItem> searchItems) :
            base(searchItems)
        {
            Name = name;
        }
    }

    public class MainViewModel
    {
    }

}
