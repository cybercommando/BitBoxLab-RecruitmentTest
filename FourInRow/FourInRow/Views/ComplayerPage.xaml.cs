using FourInRow.GameLibrary;
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
    public partial class ComplayerPage : ContentPage
    {
        public ComplayerPage()
        {
            InitializeComponent();
        }

        ComplayerLibrary library = new ComplayerLibrary();

        private void New_Clicked(object sender, EventArgs e)
        {
            library.New(this, Display, PlayerTurn, PlayerWin);
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}