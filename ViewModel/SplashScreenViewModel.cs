using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class SplashScreenViewModel : ObservableObject
    {
        public ObservableCollection<SplashScreenItem> SplashScreens { get; set; }

        [ObservableProperty]
        private bool _IsLastItem;

        public ICommand GetStartedCommand { get; set; }

        public SplashScreenViewModel()
        {
            SplashScreens = new ObservableCollection<SplashScreenItem>
            {
                new SplashScreenItem
                {
                    ImageSource = "/Splashes/splashfirst.png",
                    Title = "Share Your Recipes",
                    Description = "Every recipe is a story waiting to be told. Let’s create something delicious together.",
                    IsLastItem = false
                },

                new SplashScreenItem
                {
                    ImageSource = "/Splashes/splashsecond.png",
                    Title = "Learn to cook",
                    Description = "Cooking is an art, but all art requires knowing something about the techniques and materials.",
                    IsLastItem = false
                },

                new SplashScreenItem
                {
                    ImageSource = "/Splashes/splashthird.png",
                    Title = "Become a Chef",
                    Description = "The secret ingredient is always love, and we’ve got plenty to share.",
                    IsLastItem = true
                }
            };

            GetStartedCommand = new RelayCommand(OnGetStarted);
        }
        public void OnGetStarted()
        {
            Shell.Current.GoToAsync("///Login");
        }

        public void UpdateIsLastItem(int position)
        {
            IsLastItem = position == SplashScreens.Count - 1;
        }

        public class SplashScreenItem
        {
            public string? ImageSource { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public bool IsLastItem { get; set; }
        }
    }
}
