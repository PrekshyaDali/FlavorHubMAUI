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
            //routing for recipeforms
            Routing.RegisterRoute("AddRecipeInformation", typeof(AddRecipeInformation));
            Routing.RegisterRoute("AddRecipeIngredient", typeof(AddRecipeIngredient));
            Routing.RegisterRoute("AddRecipeDirections", typeof(AddRecipeDirections));
            Routing.RegisterRoute("AddUploads", typeof(AddUploads));


            //pages
            Routing.RegisterRoute("Gallery", typeof(Gallery));

            Routing.RegisterRoute("SecondSplashScreen", typeof(SecondSplashScreen));
            Routing.RegisterRoute("ThirdSplashScreen", typeof(ThirdSplashScreen));
            Routing.RegisterRoute("Register", typeof(Register));

        }
    }
}
