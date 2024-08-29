using CommunityToolkit.Maui.Views;

namespace FlavorHub.Views.Controls;
public partial class HeartPopUp : Popup
{
	public HeartPopUp()
	{
        InitializeComponent();
		StartAnimation();
	}
    private async void StartAnimation()
    {
        while (true)
        {
            // Animate the image to grow
            await AnimatedImage.ScaleTo(1.5, 1000, Easing.Linear);

            // Animate the image to shrink back to original size
            await AnimatedImage.ScaleTo(1, 1000, Easing.Linear);
        }
    }
}
