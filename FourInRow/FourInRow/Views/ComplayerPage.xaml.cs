﻿using System;
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

        private void New_Clicked(object sender, EventArgs e)
        {
            //Not Implemented Yet
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}