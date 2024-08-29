using FlavorHub.ViewModel;
using FlavorHub.Views;
using FlavorHub.Views.Authentication;
using FlavorHub.Views.Profile;
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
            Routing.RegisterRoute("RecipeDetailPage", typeof(RecipeDetailPage));
            Routing.RegisterRoute("RecipeListPage", typeof(RecipeListPage));


            //pages
            Routing.RegisterRoute("Gallery", typeof(Gallery));
            Routing.RegisterRoute("GalleryFullPage", typeof(GalleryFullPage));
            Routing.RegisterRoute("SecondSplashScreen", typeof(SecondSplashScreen));
            Routing.RegisterRoute("ThirdSplashScreen", typeof(ThirdSplashScreen));
            Routing.RegisterRoute("Register", typeof(Register));

            //profile
            Routing.RegisterRoute("EditProfile", typeof(EditProfile));

        }
    }
}
