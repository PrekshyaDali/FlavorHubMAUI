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
    //private static int calls = 0;

    //private void Entry_Completed(object sender, EventArgs e)
    //{
    //    System.Diagnostics.Debug.WriteLine($">>> In OnCompleted ({++calls})");
    //}
}