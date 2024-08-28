using FlavorHub.ViewModel;

namespace FlavorHub.Views;

public partial class MyRecipes : ContentPage
{
	public MyRecipes(MyRecipesViewModel myRecipesViewModel)
	{
		InitializeComponent();
		BindingContext = myRecipesViewModel;
	}
}
