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
        private readonly IRecipeRepository _RecipeRepository;
        public ICommand RefreshCommand { get; set; }
        [ObservableProperty]
        private LoginModel _LoginModel = new();

        public HomePageViewModel(FirebaseAuthClient firebaseAuthClient, IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            _FirebaseAuthClient = firebaseAuthClient;
            _UserRepository = userRepository;
            _RecipeRepository = recipeRepository;
            RefreshCommand = new AsyncRelayCommand(RefreshRecipes);
        }
     
        [ObservableProperty]
        private ObservableCollection<RecipeViewModel> _recipes;
        public async Task LoadRecipes() {
            try
            {
                var recipes = await _RecipeRepository.GetAllRecipesAsync();
                var recipeViewModels = recipes.Select(recipe => new RecipeViewModel(recipe));
                Recipes = new ObservableCollection<RecipeViewModel>(recipeViewModels);

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString);
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
