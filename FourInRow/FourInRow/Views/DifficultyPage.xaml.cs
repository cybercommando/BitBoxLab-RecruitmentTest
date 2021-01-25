using FourInRow.Models;
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
    public partial class DifficultyPage : ContentPage
    {
        public DifficultyPage()
        {
            InitializeComponent();
        }

        private void New_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "Beginner")
            {
                Application.Current.MainPage = new ComplayerPage(DifficultyLevel.BASE);
            }
            else if (btn.Text == "Easy")
            {
                Application.Current.MainPage = new ComplayerPage(DifficultyLevel.EASY);
            }
            else if (btn.Text == "Medium")
            {
                Application.Current.MainPage = new ComplayerPage(DifficultyLevel.MEDIUM);
            }
            else if (btn.Text == "Hard")
            {
                Application.Current.MainPage = new ComplayerPage(DifficultyLevel.HARD);
            }
            else if (btn.Text == "Expert")
            {
                Application.Current.MainPage = new ComplayerPage(DifficultyLevel.EXTRAHARD);
            }
        }
    }
}