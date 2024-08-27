using System.Windows.Input;

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

        public static readonly BindableProperty CookingTimeProperty =
            BindableProperty.Create(
                nameof(CookingTime),
                typeof(int),
                typeof(RecipeUIContentView),
                default(int));

        public static readonly BindableProperty DifficultyLevelProperty =
            BindableProperty.Create(
                nameof(DifficultyLevel),
                typeof(string),
                typeof(RecipeUIContentView),
                default(string));

        public static readonly BindableProperty FavoritesProperty =
         BindableProperty.Create(
             nameof(Favorites),
             typeof(bool),
             typeof(RecipeUIContentView),
             default(bool),
             defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty FavoritesCommandProperty =
           BindableProperty.Create(
               nameof(FavoritesCommand),
               typeof(ICommand),
               typeof(RecipeUIContentView),
               null);

        // Properties for binding
        public ICommand FavoritesCommand
        {
            get => (ICommand)GetValue(FavoritesCommandProperty);
            set => SetValue(FavoritesCommandProperty, value);
        }
        public bool Favorites
        {
            get => (bool)GetValue(FavoritesProperty);
            set => SetValue(FavoritesProperty, value);
        }

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

        public int CookingTime
        {
            get => (int)GetValue(CookingTimeProperty);
            set => SetValue(CookingTimeProperty, value);
        }

        public string DifficultyLevel
        {
            get => (string)GetValue(DifficultyLevelProperty);
            set => SetValue(DifficultyLevelProperty, value);
        }

        public RecipeUIContentView()
        {
            InitializeComponent();
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
