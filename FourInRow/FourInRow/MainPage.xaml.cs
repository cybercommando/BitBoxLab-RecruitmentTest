using FourInRow.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FourInRow
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void PvP_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MultiplayerPage();
        }

        private void PvP_Online_Clicked(object sender, EventArgs e)
        {
            //Comming Soon
        }

        private void CvP_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new DifficultyPage();
        }
    }
}
