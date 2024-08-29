using FlavorHub.ViewModel;

namespace FlavorHub.Views.Authentication;

public partial class Login : ContentPage
{
	private readonly LoginViewModel _LoginViewModel;
	public Login(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = _LoginViewModel = loginViewModel;
	}
}
