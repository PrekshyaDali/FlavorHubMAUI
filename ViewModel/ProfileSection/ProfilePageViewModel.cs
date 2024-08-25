using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.Repositories.Interfaces;
using Microsoft.Maui.Storage;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel.ProfileSection
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        private readonly IUserRepository _UserRepository;
        private readonly FirebaseAuthClient _FirebaseAuthClient;
        public ICommand SaveProfileDetailsCommand { get; set; }

        public ProfilePageViewModel(IUserRepository UserRepository, FirebaseAuthClient firebaseAuthClient)
        {
            _UserRepository = UserRepository;
            _FirebaseAuthClient = firebaseAuthClient;
            SaveProfileDetailsCommand = new AsyncRelayCommand(SaveProfile);
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

        // Method to refresh the profile
        public async Task RefreshProfile()
        {
            await LoadUserProfile();
        }

        // Method to load the user's profile
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
                        Email = user.Email;
                        Bio = user.Bio;
                        ProfilePicture = user.ProfilePicture;
                        User = user;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Command to save the user's profile
        [RelayCommand]
        private async Task SaveProfile()
        {
            try
            {
                if (_user != null)
                {
                    _user.UserName = UserName;
                    _user.Bio = Bio;
                    _user.Email = Email;
                    _user.ProfilePicture = ProfilePicture;

                    await _UserRepository.UpdateUserAsync(_user);
                    await Application.Current.MainPage.DisplayAlert("Success", "Profile updated", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving profile: {ex}");
            }
        }

        // Command to select a profile picture
        [RelayCommand]
        private async Task SelectProfilePicture()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Pick a profile picture"
                });

                if (result != null)
                {
                    // Save the file path returned by the file picker
                    ProfilePicture = result.FullPath;
                    User.ProfilePicture = ProfilePicture;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selecting profile picture: {ex}");
            }
        }
    }
}
