using FlavorHub.ViewModel;

namespace FlavorHub.Views;

public partial class RecipeDetailPage : ContentPage
{
	public RecipeDetailPage(RecipeDetailPageViewModel recipeDetailPageViewModel)
	{
		InitializeComponent();
		BindingContext = recipeDetailPageViewModel;
    }
}
