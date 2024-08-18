using FlavorHub.Views;
using FlavorHub.Views.Authentication;
using FlavorHub.Views.SplashScreens;

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

        protected override async void OnStart()
        {
            base.OnStart();
            // Ensure the Shell has been fully initialized before navigating
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//FirstSplashScreen");
            }
        }
    }
}
