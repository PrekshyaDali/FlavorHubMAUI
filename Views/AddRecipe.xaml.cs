using FlavorHub.ViewModel;

namespace FlavorHub.Views;

public partial class AddRecipe : ContentPage
{
	public AddRecipe(AddRecipeViewModel addRecipeViewModel)
	{
		InitializeComponent();
		BindingContext = addRecipeViewModel;
    }
}
