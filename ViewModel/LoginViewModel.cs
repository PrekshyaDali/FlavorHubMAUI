using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.Models.AuthModels;
using FlavorHub.Views.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class LoginViewModel: ObservableObject
    {
        public ICommand NavigateToRegisterCommand { get; set; }
        private readonly FirebaseAuthClient _FireBaseAuthClient;

        public LoginViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            _FireBaseAuthClient = firebaseAuthClient;
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
        }
     
        [ObservableProperty]
        private LoginModel _LoginModel = new();

        [RelayCommand]
        private async void SignIn()
        {
            try
            {
                var result = await _FireBaseAuthClient.SignInWithEmailAndPasswordAsync(_LoginModel.Email, _LoginModel.Password);

                if (!string.IsNullOrWhiteSpace(result?.User?.Info?.Email))
                {
                    await Shell.Current.GoToAsync("//HomePage");
                    await App.Current.MainPage.DisplayAlert("ok", "You havesuccessfully logged in", "ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Failed,{ex.Message}");
            }
        }

        private async void NavigateToRegister()
        {
            await Shell.Current.GoToAsync("Register");
        }

    }
}
