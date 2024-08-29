using CommunityToolkit.Mvvm.Input;
using FlavorHub.Views.SplashScreens;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class SplashScreenViewModel
    {
        public ICommand NavigateToLogin { get; }
        public ICommand NavigateToSecondSplashScreen { get; }
        public ICommand NavigateToThirdSplashScreen { get; }

        public SplashScreenViewModel()
        {
            // Initialize commands with the correct method references
            NavigateToLogin = new AsyncRelayCommand(LoginNavigate);
            NavigateToSecondSplashScreen = new AsyncRelayCommand(SecondSplashScreenNavigate);
            NavigateToThirdSplashScreen = new AsyncRelayCommand(ThirdSplashScreenNavigate);
        }
      
        private async Task LoginNavigate()
        {
            await Shell.Current.GoToAsync("///Login");
        }

        // Navigate to the second splash screen
        private async Task SecondSplashScreenNavigate()
        {
     
            try {
                Debug.WriteLine("this is start");
                await Shell.Current.GoToAsync("//SecondSplashScreen");
                Debug.WriteLine("this is end");
            }
            catch (Exception ex)
            {
              Debug.WriteLine($"Navigation to SecondSplashScreen failed: {ex.Message}");
            }
        }

        // Navigate to the third splash screen
        private async Task ThirdSplashScreenNavigate()
        {
            await Shell.Current.GoToAsync("//ThirdSplashScreen");
        }
    }
}
