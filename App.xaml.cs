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
            //LoadTheme();
        }
        //private void LoadTheme()
        //{
        //    bool isDarkMode = Preferences.Get("IsDarkMode", false);
        //    SwitchTheme(isDarkMode);
        //}
        //public void SwitchTheme(bool isDarkMode)
        //{
        //    Application.Current.Resources.MergedDictionaries.Clear();
        //    if (isDarkMode)
        //    {
        //        Application.Current.Resources.MergedDictionaries.Add(new DarkTheme());
        //    }
        //    else
        //    {
        //        Application.Current.Resources.MergedDictionaries.Add(new LightTheme());
        //    }
        //    Preferences.Set("IsDarkMode", isDarkMode);
        //}
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
