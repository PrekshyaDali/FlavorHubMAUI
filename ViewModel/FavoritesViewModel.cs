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
    public partial class FavoritesViewModel : ObservableObject
    {
        private readonly IFavoritesRepository _FavoritesRepository;
        private readonly IRecipeRepository _RecipeRepository; 
        private readonly IUserService _UserService;
        private readonly IUserRepository _UserRepository;
        public ICommand SelectionCommand { get; set; }

        [ObservableProperty]
        private RecipeViewModel _SelectedRecipe;

        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _favoritesCollection; 

        public FavoritesViewModel(IFavoritesRepository favoritesRepository, IRecipeRepository recipeRepository, IUserService userService, IUserRepository userRepository)
        {
            _FavoritesRepository = favoritesRepository;
            _RecipeRepository = recipeRepository; 
            _UserService = userService;
            _UserRepository = userRepository;
            SelectionCommand = new AsyncRelayCommand<RecipeViewModel>(OnRecipeSelected);
            _favoritesCollection = new ObservableCollection<RecipeViewModel>(); 
        }

        public async Task LoadFavorites()
        {
            try
            {
                Guid? userId = await _UserService.GetUserIdAsync();
                if (userId.HasValue)
                {
                    var favorites = await _FavoritesRepository.GetFavoritesByUserId(userId.Value);
                    if (favorites != null)
                    {
                        FavoritesCollection.Clear();
                        foreach (var favorite in favorites)
                        {
                            var recipe = await _RecipeRepository.GetRecipeByIdAsync(favorite.RecipeId);
                            if (recipe != null)
                            {
                               var recipeviewmodel = new RecipeViewModel(recipe, _UserService, _UserRepository, _FavoritesRepository);
                                FavoritesCollection.Add(recipeviewmodel);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("User ID is null. Cannot fetch favorites.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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
