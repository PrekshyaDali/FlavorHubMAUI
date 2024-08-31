using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using FlavorHub.ViewModel.RecipeFormViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class MyRecipesViewModel : ObservableObject
    {
        private readonly IUserService _UserService;
        private readonly IRecipeRepository _RecipeRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IFavoritesRepository _FavoritesRepository;
        public ICommand SelectionCommand { get; set; }

        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _RecipeCollection;

        [ObservableProperty]
        private RecipeViewModel? _SelectedRecipe;

        [ObservableProperty]
        private bool _NoMyRecipes;

        public MyRecipesViewModel(IRecipeRepository recipeRepository, IUserService userService, IFavoritesRepository favoritesRepository, IUserRepository userRepository )
        {
            _RecipeRepository = recipeRepository;
            _UserService = userService;
            _FavoritesRepository = favoritesRepository;
            _UserRepository = userRepository;
            SelectionCommand = new AsyncRelayCommand<RecipeViewModel>(OnRecipeSelected);
            RecipeCollection = new ObservableCollection<RecipeViewModel>(); 
            LoadRecipes(); 
        }

        public async Task LoadRecipes()
        {
            try
            {
                Guid? userId = await _UserService.GetUserIdAsync();
               var user = await _UserRepository.GetUserByIdAsync(userId.Value);

                if (user != null)
                {
                    var recipes = await _RecipeRepository.GetRecipeByUserIdAsync(userId.Value);
                    if (recipes != null)
                    {
                        RecipeCollection.Clear(); 
                        foreach (var recipe in recipes)
                        {
                            var recipeViewModel = new RecipeViewModel(recipe, _UserService, _UserRepository, _FavoritesRepository);
                            var favoritecount = await _FavoritesRepository.GetFavoriteCountByRecipeIdAsync(recipe.RecipeId);
                            recipeViewModel.FavoriteCount = favoritecount;
                            recipeViewModel.UserName = user.UserName;
                            recipeViewModel.ProfilePicture = user.ProfilePicture;
                            RecipeCollection.Add(recipeViewModel);
                        }
                        NoMyRecipes = RecipeCollection.Count == 0;
                    }
                    else
                    {
                        NoMyRecipes = true;
                    }
                }
                else
                {
                    Console.WriteLine("User ID is null. Cannot fetch recipes.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        //to navigate to teh detailpage
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

    }
}
