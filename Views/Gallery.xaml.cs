using FlavorHub.Services;
using FlavorHub.ViewModel;
using System.Runtime.CompilerServices;

namespace FlavorHub.Views;

public partial class Gallery : ContentPage
{
	public Gallery(GalleryViewModel galleryViewModel)
	{
		InitializeComponent();
        BindingContext = galleryViewModel;
	}
}
