using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel.ProfileSection
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        private readonly IUserRepository _UserRepository;
        private readonly FirebaseAuthClient _FirebaseAuthClient;
        private readonly string _FirebaseUid;

        public ProfilePageViewModel(IUserRepository UserRepository, FirebaseAuthClient firebaseAuthClient)
        {
            _UserRepository = UserRepository;
            _FirebaseAuthClient = firebaseAuthClient;
            LoadUserProfile();
        }

        [ObservableProperty]
        private Models.SQLiteModels.User _user;


        [ObservableProperty]
        private string _UserName;

        [ObservableProperty]
        private string _ProfilePicture;

        [ObservableProperty]
        private string _Bio;

        [ObservableProperty]
        private string _Email;

        public async Task RefreshProfile()
        {
            await LoadUserProfile();
        }

        private async Task LoadUserProfile()
        {
            try
            {
                var currentUser = _FirebaseAuthClient.User;
                if (currentUser != null)
                {
                    var user = await _UserRepository.GetUserByFirebaseUidAsync(currentUser.Uid);
                    if (user != null)
                    {
                        UserName = user.UserName;
                        Email = user?.Email;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        private async Task SaveProfile()
        {
            if (_user != null)
            {
                await _UserRepository.UpdateUserAsync(_user);
                await Application.Current.MainPage.DisplayAlert("Success", "Profile updated", "OK");
            }
        }
    }
}
