using FlavorHub.ViewModel;

namespace FlavorHub.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _ViewModel;
	public FavoritesPage(FavoritesViewModel favoritesViewModel)
	{
		InitializeComponent();
        _ViewModel = favoritesViewModel;
		BindingContext = _ViewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _ViewModel.LoadFavorites();
    }
}