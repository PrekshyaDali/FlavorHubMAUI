using FlavorHub.ViewModel;

namespace FlavorHub.Views.Authentication;

public partial class Register : ContentPage
{
    public Register()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
}