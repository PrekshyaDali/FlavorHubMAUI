using FlavorHub.ViewModel;

namespace FlavorHub.Views.Authentication;

public partial class Register : ContentPage
{
    private readonly RegisterViewModel _RegisterViewModel;
    public Register(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
        BindingContext = _RegisterViewModel = registerViewModel;
    }
}