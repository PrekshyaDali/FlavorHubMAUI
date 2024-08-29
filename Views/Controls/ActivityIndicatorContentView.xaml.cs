namespace FlavorHub.Views.Controls;

public partial class ActivityIndicatorContentView : ContentView
{
	public ActivityIndicatorContentView()
	{
        InitializeComponent();
        this.IsVisible = IsLoading;
    }

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
            propertyName: nameof(IsLoading),
            returnType: typeof(bool),
            declaringType: typeof(ActivityIndicatorContentView),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnIsLoadingChanged);

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    private static void OnIsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ActivityIndicatorContentView)bindable;
        control.IsVisible = (bool)newValue;
    }
}