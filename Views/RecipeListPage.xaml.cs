using FlavorHub.ViewModel;

namespace FlavorHub.Views;
public partial class RecipeListPage : ContentPage
{
	public RecipeListPage(RecipeListViewModel recipeListViewModel)
	{
		InitializeComponent();
		BindingContext = recipeListViewModel;
	}
}
