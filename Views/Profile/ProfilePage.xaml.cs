using FlavorHub.ViewModel.ProfileSection;

namespace FlavorHub.Views.Profile;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel profilePageViewModel)
	{
		InitializeComponent();
        BindingContext = profilePageViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ProfilePageViewModel profilePageViewModel)
        {
            await profilePageViewModel.RefreshProfile();
        }
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("EditProfile");
    }
}
