﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.Models.AuthModels;
using FlavorHub.Repositories.Interfaces;
using FlavorHub.ViewModel;
using FlavorHub.Views.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel
{
    public partial class HomePageViewModel: ObservableObject
    {
        private readonly FirebaseAuthClient _FirebaseAuthClient;

        private readonly IUserRepository _UserRepository;
        [ObservableProperty]
        private LoginModel _LoginModel = new();

        public HomePageViewModel(FirebaseAuthClient firebaseAuthClient, IUserRepository userRepository)
        {
            _FirebaseAuthClient = firebaseAuthClient;
            _UserRepository = userRepository;
        }
        [RelayCommand]
        private async Task LogOut()
        {
            // Showing the DisplayActionSheet for logout confirmation
            string action = await Application.Current.MainPage.DisplayActionSheet(
                "Are you sure you want to logout?",
                "Cancel",
                null,
                "Yes",
                "No"

            );

            // Check if the user selected "Yes"
            if (action == "Yes")
            {
                // Sign out the user
                _FirebaseAuthClient.SignOut();
                _LoginModel = new LoginModel();
                _UserRepository.ClearCachedUser();
                await Shell.Current.GoToAsync("//Login");
                
            }
        }
    }
}
