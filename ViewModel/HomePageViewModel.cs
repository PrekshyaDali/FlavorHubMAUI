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
        private readonly FirebaseAuthClient _FirebaseAuthClient;

        private readonly IUserRepository _UserRepository;
        private readonly IUserService _UserService;
        private readonly IRecipeRepository _RecipeRepository;
        private readonly IFavoritesRepository _FavoritesRepository;

        public ICommand FavoriteCommand { get; set; }
        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _recipes;
        private ObservableCollection<RecipeViewModel> _cachedRecipes;

        [ObservableProperty]
        private string? _ProfilePictureUrl;

        [ObservableProperty]
        private RecipeViewModel? _SelectedRecipe;
        public ICommand RefreshCommand { get; set; }
        public ICommand SelectionCommand { get; set; }

        [ObservableProperty]
        private LoginModel _LoginModel = new();

        public HomePageViewModel(FirebaseAuthClient firebaseAuthClient, IUserRepository userRepository, IRecipeRepository recipeRepository, IUserService userService, IFavoritesRepository favoritesRepository)
        {
            LoadProfilePictureAsync();
            _FirebaseAuthClient = firebaseAuthClient;
            _UserRepository = userRepository;
            _UserService = userService;
            _RecipeRepository = recipeRepository;
            _FavoritesRepository = favoritesRepository;
            RefreshCommand = new AsyncRelayCommand(RefreshRecipes);
            SelectionCommand = new AsyncRelayCommand<RecipeViewModel>(OnRecipeSelected);
            _FavoritesRepository = favoritesRepository;
        }   //loading the recipes from the database

        public async Task LoadRecipes()
        {
            try
            {
                // Retrieve and sorting recipes in descending order by CreatedDate
                var recipes = await _RecipeRepository.GetAllRecipesAsync();
                var sortedRecipes = recipes.OrderByDescending(r => r.CreatedDate);

                var recipeViewModels = await Task.WhenAll(sortedRecipes.Take(10).Select(async recipe =>
                {
                    var recipeViewModel = new RecipeViewModel(recipe, _UserService, _UserRepository, _FavoritesRepository);
                    await recipeViewModel.InitializeAsync();
                    return recipeViewModel;
                }));

                Recipes = new ObservableCollection<RecipeViewModel>(recipeViewModels);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task LoadProfilePictureAsync()
        {
            try
            {
                var userIdString = await SecureStorage.GetAsync("UserId");

                if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out Guid userId))
                {
                    var userDetails = await _UserRepository.GetUserByIdAsync(userId);

                    if (userDetails != null)
                    {
                        ProfilePictureUrl = userDetails.ProfilePicture;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid or missing user ID.");
                }
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
                _FirebaseAuthClient.SignOut();
                _LoginModel = new LoginModel();
                _UserRepository.ClearCachedUser();
                await Shell.Current.GoToAsync("//Login");
            }
        }
    }
}
