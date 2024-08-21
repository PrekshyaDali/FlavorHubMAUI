using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.ViewModel;
using FlavorHub.Views.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel
{
    public partial class HomePageViewModel
    {
        private readonly FirebaseAuthClient _FirebaseAuthClient;

        public HomePageViewModel(FirebaseAuthClient firebaseAuthClient)
        {
            _FirebaseAuthClient = firebaseAuthClient;
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
                await Shell.Current.GoToAsync("//Login");
                
            }
        }
    }
}
