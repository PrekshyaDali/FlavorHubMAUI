using FlavorHub.ViewModel;

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

        // Ensuring recipes are loaded when the page appears
        if (_HomePageViewModel != null)
        {
            await _HomePageViewModel.LoadRecipes();
            await _HomePageViewModel.LoadUserName(); 
        }
    }
}
