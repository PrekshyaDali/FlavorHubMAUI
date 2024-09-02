using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.Repositories.Interfaces;
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
        private Models.SQLiteModels.User user;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string profilePicture = "dot_net.png";

        [ObservableProperty]
        private string bio;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private bool isDarkMode;

        [ObservableProperty]
        private string? icon;

        public ICommand LogoutCommand { get; set; }

        public ProfilePageViewModel(IUserRepository userRepository, FirebaseAuthClient firebaseAuthClient, HomePageViewModel homePageViewModel, IUserService userService)
        {
            _UserRepository = userRepository;
            _FirebaseAuthClient = firebaseAuthClient;
            _HomePageViewModel = homePageViewModel;
            _UserService = userService;
            SaveProfileDetailsCommand = new AsyncRelayCommand(SaveProfile);
            SwitchThemeCommand = new RelayCommand(ToggleSwitch);
            LogoutCommand = new RelayCommand(SignOut);
            IsDarkMode = Application.Current.RequestedTheme == AppTheme.Dark;
            Icon = "Icons/moon.png";
        }

        public void ToggleSwitch()
        {
            if (IsDarkMode)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                IsDarkMode = false;
                Icon = "/Icons/moon.png";
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                IsDarkMode = true;
                Icon = "/Icons/sun.png";
            }
        }

        // Method to load the user's profile
        public async Task LoadUserProfile()
        {
            try
            {
                Guid? userId = await _UserService.GetUserIdAsync();
                var user = await _UserRepository.GetUserByIdAsync(userId.Value);
                if (user != null)
                {
                    UserName = user?.UserName;
                    Email = user?.Email;
                    Bio = user?.Bio;
                    ProfilePicture = user?.ProfilePicture;
                    User = user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
                await SaveProfile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selecting profile picture: {ex}");
            }
        }

        // Command to save the user's profile
        [RelayCommand]
        private async Task SaveProfile()
        {
            try
            {
                if (User != null)
                {
                    User.UserName = UserName;
                    User.Bio = Bio;
                    User.Email = Email;
                    User.ProfilePicture = ProfilePicture;

                    await _UserRepository.UpdateUserAsync(User);
                    await Application.Current.MainPage.DisplayAlert("Success", "Profile updated", "OK");
                    await LoadUserProfile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving profile: {ex}");
            }
        }

        // Sign out
        public void SignOut()
        {
            _HomePageViewModel.LogOutCommand.Execute(null);
        }
    }
}
