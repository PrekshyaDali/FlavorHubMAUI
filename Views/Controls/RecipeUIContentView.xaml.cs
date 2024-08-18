namespace FlavorHub.Views.Controls
{
    public partial class RecipeUIContentView : ContentView
    {
        public static readonly BindableProperty RecipeImageProperty =
            BindableProperty.Create(
                nameof(RecipeImage),
                typeof(ImageSource),
                typeof(RecipeUIContentView),
                default(ImageSource));

        public static readonly BindableProperty ProfileImageProperty =
            BindableProperty.Create(
                nameof(ProfileImage),
                typeof(ImageSource),
                typeof(RecipeUIContentView),
                default(ImageSource));

        public static readonly BindableProperty ContentWidthProperty =
           BindableProperty.Create(
               nameof(ContentWidth),
               typeof(int),
               typeof(RecipeUIContentView),
               300); // Default width set to 300

        public static readonly BindableProperty UserNameProperty =
            BindableProperty.Create(
                nameof(UserName),
                typeof(string),
                typeof(RecipeUIContentView),
                default(string));

        public static readonly BindableProperty RecipeFoodNameProperty =
            BindableProperty.Create(
                nameof(RecipeFoodName),
                typeof(string),
                typeof(RecipeUIContentView),
                default(string));

        // Properties for binding
        public int ContentWidth
        {
            get => (int)GetValue(ContentWidthProperty);
            set => SetValue(ContentWidthProperty, value);
        }

        public ImageSource RecipeImage
        {
            get => (ImageSource)GetValue(RecipeImageProperty);
            set => SetValue(RecipeImageProperty, value);
        }

        public ImageSource ProfileImage
        {
            get => (ImageSource)GetValue(ProfileImageProperty);
            set => SetValue(ProfileImageProperty, value);
        }

        public string UserName
        {
            get => (string)GetValue(UserNameProperty);
            set => SetValue(UserNameProperty, value);
        }

        public string RecipeFoodName
        {
            get => (string)GetValue(RecipeFoodNameProperty);
            set => SetValue(RecipeFoodNameProperty, value);
        }

        public RecipeUIContentView()
        {
            InitializeComponent();
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
