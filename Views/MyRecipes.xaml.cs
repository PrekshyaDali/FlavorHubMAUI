using FlavorHub.ViewModel;

namespace FlavorHub.Views;

public partial class MyRecipes : ContentPage
{
    private readonly MyRecipesViewModel _myRecipesViewModel;
    public MyRecipes(MyRecipesViewModel myRecipesViewModel)
	{
		InitializeComponent();
        _myRecipesViewModel = myRecipesViewModel;
        BindingContext = _myRecipesViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _myRecipesViewModel.LoadRecipes();
    }
}
