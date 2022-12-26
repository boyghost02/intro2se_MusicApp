using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPageView : TabbedPage
    {
        public DashboardPageView ()
        {
            InitializeComponent();
        }

        private void NavigationPage_Appearing(object sender, EventArgs e)
        {

        }
    }
}