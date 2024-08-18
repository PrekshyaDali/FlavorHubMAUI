using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FlavorHub.ViewModel;
using FlavorHub.Views.Authentication;

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
                ApiKey= "\r\nAIzaSyCVa-EY5LQBifY8gKCcsS7AYY3qpOCyCeo",
                AuthDomain = "flavorhub-authentication.firebaseapp.com",
                Providers = [new EmailProvider()]
            }));

            //viewModels
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();

            //pages
            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<Login>();

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
