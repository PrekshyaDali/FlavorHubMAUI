using FlavorHub.ViewModel;

namespace FlavorHub.Views;
[QueryProperty(nameof(GalleryFullPageViewModel.PhotoUrl), "PhotoUrl")]
public partial class GalleryFullPage : ContentPage
{
	public GalleryFullPage()
	{
		InitializeComponent();
	}
    public string PhotoUrl
    {
        get => ((GalleryFullPageViewModel)BindingContext).PhotoUrl;
        set => ((GalleryFullPageViewModel)BindingContext).PhotoUrl = value;
    }
}
