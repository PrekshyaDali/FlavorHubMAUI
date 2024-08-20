using FlavorHub.Services;
using FlavorHub.ViewModel;
using System.Runtime.CompilerServices;

namespace FlavorHub.Views;

public partial class Gallery : ContentPage
{
	private readonly GalleryViewModel _GalleryViewModel;
	public Gallery(GalleryViewModel galleryViewModel)
	{
		InitializeComponent();
        BindingContext = _GalleryViewModel = galleryViewModel;
	}

}