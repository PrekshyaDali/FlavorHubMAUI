using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.Views.Authentication;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlavorHub.Models.AuthModels;

namespace FlavorHub.ViewModel
{
    public partial class RegisterViewModel: ObservableObject
    {
        private readonly FirebaseAuthClient _FirebaseAuthClient;

        public RegisterViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            _FirebaseAuthClient = firebaseAuthClient;
        }

        [ObservableProperty]
        private RegisterModel _RegisterModel = new();

        //Command for signup
        [RelayCommand]
        private async Task SignUp()
        {
            try
            {
                var result = await _FirebaseAuthClient.CreateUserWithEmailAndPasswordAsync(
                 _RegisterModel.Email,
                 _RegisterModel.Password,
                 _RegisterModel.UserName
             );

                if (!string.IsNullOrWhiteSpace(result?.User?.Info?.Email))
                {
                    await Shell.Current.GoToAsync("Login");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Signup failed, {ex.ToString()}");
            }
        }

    }
}
