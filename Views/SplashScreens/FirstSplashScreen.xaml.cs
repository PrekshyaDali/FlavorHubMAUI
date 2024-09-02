using FlavorHub.ViewModel;

namespace FlavorHub.Views.SplashScreens;

public partial class FirstSplashScreen : ContentPage
{
	public FirstSplashScreen()
	{
		InitializeComponent();
	}

    private void CarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
    {
        var viewModel = BindingContext as SplashScreenViewModel;
        viewModel?.UpdateIsLastItem(e.CurrentPosition);
    }
}
