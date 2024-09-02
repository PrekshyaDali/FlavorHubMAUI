using Firebase.Auth;
using FlavorHub.NewFolder;
using FlavorHub.ViewModel.ProfileSection;
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
            await CheckUserSesssionAsync();
        }

        private async Task CheckUserSesssionAsync()
        {
            var userId = await SecureStorage.GetAsync("UserId");

            if (!string.IsNullOrEmpty(userId))
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await Shell.Current.GoToAsync("//FirstSplashScreen");
            }
        }
    }
}
