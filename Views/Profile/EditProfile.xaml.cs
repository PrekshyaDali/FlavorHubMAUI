using FlavorHub.ViewModel.ProfileSection;

namespace FlavorHub.Views.Profile;

public partial class EditProfile : ContentPage
{
	public EditProfile(ProfilePageViewModel profilePageViewModel)
	{
		InitializeComponent();
		BindingContext = profilePageViewModel;
	}
}
