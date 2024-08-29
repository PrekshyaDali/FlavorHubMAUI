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
using FlavorHub.Repositories.Interfaces;

namespace FlavorHub.ViewModel
{
    public partial class RegisterViewModel: ObservableObject
    {
        private readonly FirebaseAuthClient _FirebaseAuthClient;
        private readonly IUserRepository _UserRepository;

        public RegisterViewModel(FirebaseAuthClient firebaseAuthClient, IUserRepository userRepository)
        {
            _FirebaseAuthClient = firebaseAuthClient;
            _UserRepository = userRepository;
        }

        [ObservableProperty]
        private RegisterModel _RegisterModel = new();
        [ObservableProperty]
        private string _EmailErrorMessage;
        [ObservableProperty]
        private string _PasswordErrorMessage;

        [ObservableProperty]
        private bool _IsBusy;

        //Command for signup
        [RelayCommand]
        private async Task SignUp()
        {
            // Reset error messages
            EmailErrorMessage = string.Empty;
            PasswordErrorMessage = string.Empty;
            try
            {
                var result = await _FirebaseAuthClient.CreateUserWithEmailAndPasswordAsync(
                 _RegisterModel.Email,
                 _RegisterModel.Password,
                 _RegisterModel.UserName
             );

                if (!string.IsNullOrWhiteSpace(result?.User?.Info?.Email))
                {
                    var user = new Models.SQLiteModels.User
                    {
                        FirebaseUID = result.User.Uid,
                        UserName = _RegisterModel.UserName,
                        Email = _RegisterModel?.Email,
                        CreatedDate = DateTime.UtcNow
                    };
                    IsBusy = true;
                    await _UserRepository.CreateUserAysnc(user);
                    await Application.Current.MainPage.DisplayAlert("Success", "You are registered successfully", "oks");
                    await Shell.Current.GoToAsync("//Login");
                }
            }
            catch (Exception ex)
            {
                var _d = ex.Data;
                Console.WriteLine($"Signup failed, {ex.ToString()}");
                await Application.Current.MainPage.DisplayAlert("Failed", "Failed creating your account", "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
