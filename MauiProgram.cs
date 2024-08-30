using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FlavorHub.ViewModel;
using FlavorHub.Views.Authentication;
using FlavorHub.Models;
using FlavorHub.Views;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using FlavorHub.Services;
using FlavorHub.Views.Profile;
using FlavorHub.ViewModel.ProfileSection;
using SQLite;
using FlavorHub.Data;
using FlavorHub.Repositories;
using FlavorHub.ViewModel.RecipeFormViewModels;
using FlavorHub.Repositories.Interfaces;

namespace FlavorHub
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig(){
                ApiKey= "AIzaSyBQBIS3Z8z6ENR9j1YNsDXdA4JtfAAL1fI",
                AuthDomain = "flavorhub-authentication.firebaseapp.com",
                Providers = new FirebaseAuthProvider[] { new EmailProvider() }
            }));

            //viewModels
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<GalleryViewModel>();
            builder.Services.AddSingleton<ProfilePageViewModel>();
            builder.Services.AddSingleton<AddRecipeViewModel>();
            builder.Services.AddSingleton<AddUploadViewModel>();
            builder.Services.AddSingleton<DirectionViewModel>();
            builder.Services.AddSingleton<IngredientViewModel>();
            builder.Services.AddSingleton<InfomationViewModel>();
            builder.Services.AddSingleton<RecipeViewModel>();
            builder.Services.AddSingleton<RecipeDetailPageViewModel>();
            builder.Services.AddSingleton<MyRecipesViewModel>();
            builder.Services.AddSingleton<FavoritesViewModel>();
            builder.Services.AddSingleton<RecipeListViewModel>();

            //pages
            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<Gallery>();
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<EditProfile>();
            builder.Services.AddSingleton<RecipeDetailPage>();
            builder.Services.AddSingleton<MyRecipes>();
            builder.Services.AddSingleton<FavoritesPage>();
            builder.Services.AddSingleton<RecipeListPage>();
            //recipe pages

            builder.Services.AddSingleton<AddRecipe>();
            builder.Services.AddSingleton<AddRecipeInformation>();
            builder.Services.AddSingleton<AddRecipeDirections>();
            builder.Services.AddSingleton<AddRecipeIngredient>();
            builder.Services.AddSingleton<AddUploads>();

            //services
            builder.Services.AddSingleton<IUserService, UserService>();
            // Load configuration
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "FlavorHub.appsettings.json";

            //SQLLite
            builder.Services.AddSingleton(_ =>
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FlavorHub.db3");
                return new SQLiteAsyncConnection(dbPath);
            });

            builder.Services.AddSingleton<AppDbContext>();
            builder.Services.AddSingleton<Repositories.Interfaces.IUserRepository, UserRepository>();
            builder.Services.AddSingleton<Repositories.Interfaces.IRecipeRepository, RecipeRepository>();
            builder.Services.AddSingleton<Repositories.Interfaces.ICommentsRepository, CommentRepository>();
            builder.Services.AddSingleton<Repositories.Interfaces.IFavoritesRepository, FavoritesRepository>();


            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("The configuration file 'appsettings.json' was not found.");
                }

                var configuration = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                var pexelsApiKey = configuration["PexelsApiKey"];
                builder.Services.AddSingleton(new PexelsService(pexelsApiKey));
            }

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
            
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

#endif
            });

            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(DatePicker), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList= Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping(nameof(TimePicker), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList= Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });
            Microsoft.Maui.Handlers.StepperHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
            {
#if ANDROID
             Android.Widget.LinearLayout linearLayout= handler.PlatformView as Android.Widget.LinearLayout;
            linearLayout.GetChildAt(0).SetBackgroundColor(Android.Graphics.Color.Transparent);
            linearLayout.GetChildAt(1).SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
