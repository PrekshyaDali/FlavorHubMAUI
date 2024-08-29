using FlavorHub.ViewModel.RecipeFormViewModels;

namespace FlavorHub.Views;

public partial class AddRecipeInformation : ContentPage
{
	public AddRecipeInformation(InfomationViewModel inforamtionviewmodel)
	{
		InitializeComponent();
		BindingContext = inforamtionviewmodel;
		
	}
}
