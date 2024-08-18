using FlavorHub.ViewModel;
using FlavorHub.Views;
using FlavorHub.Views.Authentication;
using FlavorHub.Views.SplashScreens;

namespace FlavorHub
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("AddRecipeInformation", typeof(AddRecipeInformation));
            Routing.RegisterRoute("AddRecipeIngredient", typeof(AddRecipeIngredient));
            Routing.RegisterRoute("AddRecipeDirections", typeof(AddRecipeDirections));
            Routing.RegisterRoute("SecondSplashScreen", typeof(SecondSplashScreen));
            Routing.RegisterRoute("ThirdSplashScreen", typeof(ThirdSplashScreen));
            Routing.RegisterRoute("Login", typeof(Login));



        }
    }
}
