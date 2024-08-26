using FlavorHub.ViewModel;

namespace FlavorHub.Views;

public partial class RecipeDetailPage : ContentPage
{
	public RecipeDetailPage()
	{
		InitializeComponent();
		BindingContext = new RecipeDetailPageViewModel();
	}
}
