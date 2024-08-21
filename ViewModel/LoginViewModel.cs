using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.Models.AuthModels;
using FlavorHub.Views.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
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
        private async Task SignIn()
        {
            try
            {
                var result = await _FireBaseAuthClient.SignInWithEmailAndPasswordAsync(_LoginModel.Email, _LoginModel.Password);

                if (!string.IsNullOrWhiteSpace(result?.User?.Info?.Email))
                {
                    ClearLoginModel();
                    await Shell.Current.GoToAsync("//HomePage");
                    await Application.Current.MainPage.DisplayAlert("ok", "You have successfully logged in", "ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Failed,{ex.Message}");
            }
        }
        private void ClearLoginModel() {
            _LoginModel = new LoginModel();
        }
          
        private async void NavigateToRegister()
        {
            await Shell.Current.GoToAsync("Register");
        }
    }
}
