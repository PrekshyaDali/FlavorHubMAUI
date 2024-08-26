using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using FlavorHub.Models.AuthModels;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using FlavorHub.ViewModel;
using FlavorHub.ViewModel.RecipeFormViewModels;
using FlavorHub.Views.Authentication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class HomePageViewModel: ObservableObject
    {
        private readonly FirebaseAuthClient _FirebaseAuthClient;

        private readonly IUserRepository _UserRepository;
        private readonly IUserService _UserService;
        private readonly IRecipeRepository _RecipeRepository;
        public ICommand RefreshCommand { get; set; }
        public ICommand SelectionCommand { get; set; }

        [ObservableProperty]
        private LoginModel _LoginModel = new();

        public HomePageViewModel(FirebaseAuthClient firebaseAuthClient, IUserRepository userRepository, IRecipeRepository recipeRepository, IUserService userService)
        {
            _FirebaseAuthClient = firebaseAuthClient;
            _UserRepository = userRepository;
            _UserService = userService;
            _RecipeRepository = recipeRepository;
            RefreshCommand = new AsyncRelayCommand(RefreshRecipes);
            SelectionCommand = new AsyncRelayCommand<RecipeViewModel>(OnRecipeSelected);
        }
        [RelayCommand]
        private async Task OnRecipeSelected(RecipeViewModel selectedRecipe)
        {
            if (selectedRecipe != null)
            {
                // Navigate to the RecipeDetailPage, passing the selected recipe as a parameter
                await Shell.Current.GoToAsync("///RecipeDetailPage", true, new Dictionary<string, object>
            {
                { "SelectedRecipe", selectedRecipe }
            });
            }
        }

        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _recipes;
        private ObservableCollection<RecipeViewModel> _cachedRecipes;

        //loading the recipes from the database
        public async Task LoadRecipes()
        {
            try
            {
                // Retrieve all recipes from the repository
                var recipes = await _RecipeRepository.GetAllRecipesAsync();

                // Log the retrieved recipes
                Console.WriteLine("Retrieved recipes:");
                foreach (var recipe in recipes)
                {
                    Console.WriteLine($"{recipe.Title} - {recipe.CreatedDate}");
                }

                // Sort recipes in descending order by CreatedDate
                var sortedRecipes = recipes.OrderByDescending(r => r.CreatedDate).ToList();

                // Log the sorted recipes
                Console.WriteLine("Sorted recipes:");
                foreach (var recipe in sortedRecipes)
                {
                    Console.WriteLine($"{recipe.Title} - {recipe.CreatedDate}");
                }

                var recipeViewModels = new List<RecipeViewModel>();

                // Create RecipeViewModel instances for each sorted recipe
                foreach (var recipe in sortedRecipes)
                {
                    var recipeViewModel = new RecipeViewModel(recipe, _UserService, _UserRepository);
                    await recipeViewModel.InitializeAsync();
                    recipeViewModels.Add(recipeViewModel);
                }

                // Cache the top 10 recipes in descending order
                Recipes = new ObservableCollection<RecipeViewModel>(recipeViewModels.Take(10));

                // Log the cached recipes
                Console.WriteLine("Cached recipes:");
                foreach (var viewModel in _cachedRecipes)
                {
                    Console.WriteLine($"{viewModel.Title} - {viewModel.CreatedDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        private async Task RefreshRecipes()
        {
            await LoadRecipes();
        }

        [RelayCommand]
        private async Task LogOut()
        {
            // Showing the DisplayActionSheet for logout confirmation
            string action = await Application.Current.MainPage.DisplayActionSheet(
                "Are you sure you want to logout?",
                "Cancel",
                null,
                "Yes",
                "No"

            );

            // Check if the user selected "Yes"
            if (action == "Yes")
            {
                // Sign out the user
                _FirebaseAuthClient.SignOut();
                _LoginModel = new LoginModel();
                _UserRepository.ClearCachedUser();
                await Shell.Current.GoToAsync("//Login");
            }
        }
    }
}
