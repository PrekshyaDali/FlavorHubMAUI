﻿using FlavorHub.Views;
using FlavorHub.Views.Authentication;

namespace FlavorHub
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //MainPage = new NavigationPage(new HomePage());
        }
    }
}
