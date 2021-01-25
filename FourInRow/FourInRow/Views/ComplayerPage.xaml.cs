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
        ComplayerLibrary library = new ComplayerLibrary();

        public ComplayerPage()
        {
            InitializeComponent();
        }

        public ComplayerPage(DifficultyLevel _difficultyLevel)
        {
            InitializeComponent();
            var text = "";

            if (_difficultyLevel == DifficultyLevel.BASE)
            {
                text = "(Beginner)";
            }
            else if (_difficultyLevel == DifficultyLevel.EASY)
            {
                text = "(EASY)";
            }
            else if (_difficultyLevel == DifficultyLevel.MEDIUM)
            {
                text = "(MEDIUM)";
            }
            else if (_difficultyLevel == DifficultyLevel.HARD)
            {
                text = "(HARD)";
            }
            else if (_difficultyLevel == DifficultyLevel.EXTRAHARD)
            {
                text = "(EXPERT)";
            }

            HeaderLabel.Text += text;
            library.New(this, Display, _difficultyLevel, PlayerTurn, PlayerWin);
        }

        private void New_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new DifficultyPage();
        }
    }
}