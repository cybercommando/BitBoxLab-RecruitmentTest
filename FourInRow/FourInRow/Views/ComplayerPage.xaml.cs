using FourInRow.GameLibrary;
using FourInRow.Models;
using System;
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
            library.New(this, Display, DifficultyLevel.EASY,PlayerTurn, PlayerWin);
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}