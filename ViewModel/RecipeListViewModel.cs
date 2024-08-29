using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using FlavorHub.ViewModel.RecipeFormViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class RecipeListViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IUserService _UserService;
        private readonly IUserRepository _UserRepository;
        private readonly IFavoritesRepository _FavoritesRepository;

        private List<RecipeViewModel> _cachedRecipes; 

        [ObservableProperty]
        private RecipeViewModel _selectedRecipe;

        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _recipes;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _selectedCategory;

        [ObservableProperty]
        private string _selectedCookingTime;

        [ObservableProperty]
        private string _selectedDifficultyLevel;

        // Commands for Search and Filter
        public ICommand SearchCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand SelectionCommand  { get; }
        public ICommand ClearDifficultyCommand { get; }
        public ICommand ClearCookingTimeCommand { get; }

        public ObservableCollection<string> DifficultyLevels { get; }
        public ObservableCollection<string> CookingTimes { get; }

        public RecipeListViewModel(IUserService userService, IUserRepository userRepository, IFavoritesRepository favoritesRepository)
        {
            _UserService = userService;
            _UserRepository = userRepository;
            _FavoritesRepository = favoritesRepository;

            _cachedRecipes = new List<RecipeViewModel>();
            Recipes = new ObservableCollection<RecipeViewModel>();

            DifficultyLevels = new ObservableCollection<string> { "Easy", "Medium", "Difficult" };
            CookingTimes = new ObservableCollection<string> { "Less than 1 hour", "Less than 2 hours", "2 hours or more" };

            SelectionCommand = new AsyncRelayCommand<RecipeViewModel>(OnRecipeSelected);
            SearchCommand = new RelayCommand(ApplySearchAndFilter);
            FilterCommand = new RelayCommand(ApplySearchAndFilter);
            ClearDifficultyCommand = new RelayCommand(ClearDifficultyFilter);
            ClearCookingTimeCommand = new RelayCommand(ClearCookingTimeFilter);
        }

        private void ApplySearchAndFilter()
        {
            var filteredRecipes = _cachedRecipes.Where(r =>
                (string.IsNullOrEmpty(SearchText) || r.Title.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)) &&
                (SelectedCookingTime == "Less than 1 hour" ? r.CookingTime < 60 :
                 SelectedCookingTime == "Less than 2 hours" ? r.CookingTime < 120 :
                 SelectedCookingTime == "2 hours or more" ? r.CookingTime >= 120 : true) &&
                (string.IsNullOrEmpty(SelectedDifficultyLevel) || r.DifficultyLevel == SelectedDifficultyLevel)
            );

            Recipes = new ObservableCollection<RecipeViewModel>(filteredRecipes);
        }
        private void ClearDifficultyFilter()
        {
            SelectedDifficultyLevel = null;
            ApplySearchAndFilter();
        }

        private void ClearCookingTimeFilter()
        {
            SelectedCookingTime = null;
            ApplySearchAndFilter();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Recipes"))
            {
                var recipes = query["Recipes"] as List<Recipe>;

                var recipeViewModels = await Task.WhenAll(recipes.Select(async recipe =>
                {
                    var recipeViewModel = new RecipeViewModel(recipe, _UserService, _UserRepository, _FavoritesRepository);
                    await recipeViewModel.InitializeAsync();
                    return recipeViewModel;
                }));

                _cachedRecipes = recipeViewModels.ToList();
                Recipes = new ObservableCollection<RecipeViewModel>(_cachedRecipes);
            }
        }

        [RelayCommand]
        private async Task OnRecipeSelected(RecipeViewModel selectedRecipe)
        {
            if (selectedRecipe != null)
            {
                SelectedRecipe = selectedRecipe;
                await Shell.Current.GoToAsync("RecipeDetailPage", true, new Dictionary<string, object>
                {
                    { "SelectedRecipe", selectedRecipe }
                });
            }
        }
    }
}
