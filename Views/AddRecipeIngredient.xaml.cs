using FlavorHub.ViewModel.RecipeFormViewModels;

namespace FlavorHub.Views;

public partial class AddRecipeIngredient : ContentPage
{
	public AddRecipeIngredient(IngredientViewModel ingredientViewModel)
	{
		InitializeComponent();
		BindingContext = ingredientViewModel;
	}
}
