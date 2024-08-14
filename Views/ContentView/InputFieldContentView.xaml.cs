using Microsoft.Maui.Controls;

namespace FlavorHub.Views.ContentView
{
    public partial class InputFieldContentView : IContentView
    {
        // Bindable property for entry fields
        public static readonly BindableProperty EntryDataProperty = BindableProperty.Create(
            nameof(EntryData), typeof(string), typeof(InputFieldContentView), string.Empty);

        public static readonly BindableProperty IsPasswordProperty =
           BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(InputFieldContentView), false);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard), typeof(Keyboard), typeof(InputFieldContentView), Keyboard.Default);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder), typeof(string), typeof(InputFieldContentView), string.Empty);

        public string EntryData
        {
            get => (string)GetValue(EntryDataProperty);
            set => SetValue(EntryDataProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public InputFieldContentView()
        {
            InitializeComponent();
        }
    }
}
