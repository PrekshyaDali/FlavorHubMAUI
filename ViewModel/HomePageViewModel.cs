using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Firebase.Auth;
using FlavorHub.Models;
using FlavorHub.Models.AuthModels;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using FlavorHub.ViewModel;
using FlavorHub.ViewModel.RecipeFormViewModels;
using FlavorHub.Views.Authentication;
using Microsoft.Maui.Controls.Internals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUserService _UserService;
        private readonly IRecipeRepository _RecipeRepository;
        private readonly IFavoritesRepository _FavoritesRepository;

        public ICommand FavoriteCommand { get; set; }
        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _recipes;

        private ObservableCollection<RecipeViewModel> _cachedRecipes;

        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _popularRecipes;

        [ObservableProperty]
        private bool _IsBusy;

        [ObservableProperty]
        private string? _Users;

        [ObservableProperty]
        private RecipeViewModel? _SelectedRecipe;
        public ICommand RefreshCommand { get; set; }
        public ICommand SelectionCommand { get; set; }
        public ICommand NavigateToViewAllCommand { get; set; }

        [ObservableProperty]
        private LoginModel _LoginModel = new();

        public HomePageViewModel( IUserRepository userRepository, IRecipeRepository recipeRepository, IUserService userService, IFavoritesRepository favoritesRepository)
        {
            _UserRepository = userRepository;
            _UserService = userService;
            _RecipeRepository = recipeRepository;
            _FavoritesRepository = favoritesRepository;
            RefreshCommand = new AsyncRelayCommand(RefreshRecipes);
            SelectionCommand = new AsyncRelayCommand<RecipeViewModel>(OnRecipeSelected);
            NavigateToViewAllCommand = new AsyncRelayCommand(ViewAllRecipes);
            _FavoritesRepository = favoritesRepository;
        }

        //loading the recipes from the database
        public async Task LoadRecipes()
        {
            IsBusy = true;
            try
            {
                var recipes = await _RecipeRepository.GetAllRecipesAsync();
                var sortedRecipes = recipes.OrderByDescending(r => r.CreatedDate);

                var recipeViewModels = await Task.WhenAll(sortedRecipes.Take(10).Select(async recipe =>
                {
                    var recipeViewModel = new RecipeViewModel(recipe, _UserService, _UserRepository, _FavoritesRepository);
                    await recipeViewModel.InitializeAsync();
                    return recipeViewModel;
                }));

                var sortedByFavorites = recipeViewModels.OrderByDescending(rv => rv.FavoriteCount).Take(10);

                PopularRecipes = new ObservableCollection<RecipeViewModel>(sortedByFavorites);
                Recipes = new ObservableCollection<RecipeViewModel>(recipeViewModels);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }


        [RelayCommand]
        private async Task ViewAllRecipes()
        {
            // Load and sort the recipes
            var recipes = await _RecipeRepository.GetAllRecipesAsync();
            var sortedRecipes = recipes.OrderByDescending(r => r.CreatedDate).ToList();

            // Pass the sorted list to the RecipeListPage
            await Shell.Current.GoToAsync("RecipeListPage", true, new Dictionary<string, object>
    {
        { "Recipes", sortedRecipes }
        });
        }

        public async Task LoadUserName()
        {
            try
            {

                Guid? userId = await _UserService.GetUserIdAsync();
                var user = await _UserRepository.GetUserByIdAsync(userId.Value);
                _Users = user.UserName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading profile picture: {ex}");
            }
        }

        [RelayCommand]
        private async Task OnRecipeSelected(RecipeViewModel selectedRecipe)
        {
            if (selectedRecipe != null)
            {
                SelectedRecipe = selectedRecipe;
                // Navigate to the RecipeDetailPage, passing the selected recipe as a parameter
                await Shell.Current.GoToAsync("RecipeDetailPage", true, new Dictionary<string, object>
            {
                { "SelectedRecipe", selectedRecipe }
            });
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
                //_FirebaseAuthClient.SignOut();
                _LoginModel = new LoginModel();
                _UserRepository.ClearCachedUser();
                SecureStorage.RemoveAll();
                await Shell.Current.GoToAsync("//Login");
            }
        }
    }
}
