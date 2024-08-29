using FlavorHub.NewFolder;
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
            
        }
        protected override async void OnStart()
        {
            base.OnStart();
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//FirstSplashScreen");
            }
        }
    }
}
