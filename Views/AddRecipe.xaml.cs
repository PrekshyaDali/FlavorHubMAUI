using FlavorHub.ViewModel;

namespace FlavorHub.Views;

public partial class AddRecipe : ContentPage
{
	public AddRecipe()
	{
		InitializeComponent();
        BindingContext = new AddRecipeViewModel();
    }
}
