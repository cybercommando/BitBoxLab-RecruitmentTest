using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourInRow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiplayerPage : ContentPage
    {
        public MultiplayerPage()
        {
            InitializeComponent();
        }

        Library library = new Library();

        private void New_Clicked(object sender, EventArgs e)
        {
            library.New(this, Display);
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

    }
}