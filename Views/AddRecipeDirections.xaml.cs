using FlavorHub.ViewModel.RecipeFormViewModels;

namespace FlavorHub.Views;

public partial class AddRecipeDirections : ContentPage
{
	public AddRecipeDirections(DirectionViewModel directionViewModel)
	{
		InitializeComponent();
		BindingContext = directionViewModel;
	}
}
