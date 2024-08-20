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
                Providers = [new EmailProvider()]
            }));

            //viewModels
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<GalleryViewModel>();

            //pages
            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<Gallery>();

            // Load configuration
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "FlavorHub.appsettings.json"; // Adjust the path based on your file's namespace and location

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
