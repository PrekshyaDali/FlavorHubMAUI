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
    public partial class RecipeListViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IUserService _UserService;
        private readonly IUserRepository _UserRepository;
        private readonly IFavoritesRepository _FavoritesRepository;

        [ObservableProperty]
        private RecipeViewModel _SelectedRecipe;
        public ICommand SelectionCommand { get; set; }

        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _recipes;

        public RecipeListViewModel(IUserService userService, IUserRepository userRepository, IFavoritesRepository favoritesRepository)
        {
            _UserService = userService;
            _UserRepository = userRepository;
            _FavoritesRepository = favoritesRepository;
            SelectionCommand = new AsyncRelayCommand<RecipeViewModel>(OnRecipeSelected);
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

                Recipes = new ObservableCollection<RecipeViewModel>(recipeViewModels);
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
