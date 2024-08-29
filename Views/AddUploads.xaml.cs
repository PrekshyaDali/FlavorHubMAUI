using FlavorHub.ViewModel.RecipeFormViewModels;

namespace FlavorHub.Views;

public partial class AddUploads : ContentPage
{
	public AddUploads(AddUploadViewModel addUploadViewModel)
	{
		InitializeComponent();
		BindingContext = addUploadViewModel;
	}
}
