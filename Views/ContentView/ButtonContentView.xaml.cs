using System.Windows.Input;

namespace FlavorHub.Views.ContentView;

public partial class ButtonContentView : IContentView
{
    public static readonly BindableProperty ButtonTextProperty =
            BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(ButtonContentView), default(string));

    // Define the bindable property for the button text color
    public static readonly BindableProperty ButtonTextColorProperty =
        BindableProperty.Create(nameof(ButtonTextColor), typeof(Color), typeof(ButtonContentView), Colors.Black);

    // Define the bindable property for the button command
    public static readonly BindableProperty ButtonCommandProperty =
        BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(ButtonContentView));

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public Color ButtonTextColor
    {
        get => (Color)GetValue(ButtonTextColorProperty);
        set => SetValue(ButtonTextColorProperty, value);
    }

    public ICommand ButtonCommand
    {
        get => (ICommand)GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }

    public ButtonContentView()
	{
		InitializeComponent();
	}
}