using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Views.SplashScreens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class SplashScreenViewModel: ObservableObject
    {
        public ObservableCollection<SplashScreenItem> SplashScreens {  get; set; }
        public SplashScreenViewModel()
        {
            SplashScreens = new ObservableCollection<SplashScreenItem>
            {
                new SplashScreenItem
                {
                    ImageSource = "/Splashes/splashfirst.png",
                    Title = "Share Your Recipes",
                    Description = "Every recipe is a story waiting to be told. Let’s create something delicious together."
                },

                new SplashScreenItem
                {
                    ImageSource = "/Splashes/splashsecond.png",
                    Title = "Learn to cook",
                    Description = "Cooking is an art, but all art requires knowing something about the techniques and materials."
                },

                 new SplashScreenItem
                {
                    ImageSource = "/Splashes/splashthird.png",
                    Title = "Become a Chef",
                    Description = "The secret ingredient is always love, and we’ve got plenty to share."
                }
            };
        }

        private async Task LoginNavigate()
        {
            await Shell.Current.GoToAsync("///Login");
        }

        public class SplashScreenItem
        {
            public string ImageSource { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }
}
