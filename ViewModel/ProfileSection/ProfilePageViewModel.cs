using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Firebase.Auth;
using FlavorHub.Models;
using FlavorHub.NewFolder;
using FlavorHub.Repositories.Interfaces;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Storage;
using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel.ProfileSection
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        private readonly IUserRepository _UserRepository;
        private readonly FirebaseAuthClient _FirebaseAuthClient;
        private readonly IUserService _UserService;
        private HomePageViewModel _HomePageViewModel;
        public ICommand SaveProfileDetailsCommand { get; set; }
        public ICommand SwitchThemeCommand { get; set; }

        [ObservableProperty]
        private Models.SQLiteModels.User _user;

        [ObservableProperty]
        private string _UserName;

        [ObservableProperty]
        private string _ProfilePicture = "dot_net.png";

        [ObservableProperty]
        private string _Bio;

        [ObservableProperty]
        private string _Email;

        [ObservableProperty]
        private bool _IsDarkMode;

        public ICommand LogoutCommand;

        public ProfilePageViewModel(IUserRepository UserRepository, FirebaseAuthClient firebaseAuthClient, HomePageViewModel homePageViewModel, IUserService userService)
        {
            LoadUserProfile();
            _UserRepository = UserRepository;
            _FirebaseAuthClient = firebaseAuthClient;
            _HomePageViewModel = homePageViewModel;
            _UserService = userService;
            SaveProfileDetailsCommand = new AsyncRelayCommand(SaveProfile);
            SwitchThemeCommand = new RelayCommand(ToggleSwitch);
            LogoutCommand = new RelayCommand(SignOut);
            _IsDarkMode = Application.Current.RequestedTheme == AppTheme.Dark;
        }
        public void ToggleSwitch()
        {
            if (_IsDarkMode) 
           {
                Application.Current.UserAppTheme = AppTheme.Light;   
                IsDarkMode = false;
           }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                IsDarkMode = true;
            }
        }

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
                Guid? userId = await _UserService.GetUserIdAsync();
                var user = await _UserRepository.GetUserByIdAsync(userId.Value);
                if (user != null)
                    {
                        UserName = user.
                        Email = user.Email;
                        Bio = user.Bio;
                        ProfilePicture = user.ProfilePicture;
                        User = user;
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

        //sign out
        public void SignOut()
        {
            _HomePageViewModel.LogOutCommand.Execute(null);
        }
    }
}
