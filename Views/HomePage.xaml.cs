using FlavorHub.ViewModel;
using FlavorHub.ViewModel.ProfileSection;

namespace FlavorHub.Views;

public partial class HomePage : ContentPage
{
	private readonly HomePageViewModel _HomePageViewModel;
    public HomePage(HomePageViewModel homePageViewModel)
	{
		InitializeComponent();
		BindingContext = _HomePageViewModel = homePageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

<<<<<<< HEAD
        // Ensuring recipes are loaded when the page appears
=======
>>>>>>> c432d41c467b3bc301cc3aecd3a6bce4d12219b8
        if (_HomePageViewModel != null)
        {
            await _HomePageViewModel.LoadRecipes();
            await _HomePageViewModel.LoadUserName();
        }
    }
}
