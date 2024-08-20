using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Models.RecipeFormModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public partial class IngredientViewModel: ObservableObject
    {
        public ICommand NavigateToDirections { get; set; }

        [ObservableProperty]
        private ObservableCollection<IngredientModel> _Ingredients = new ObservableCollection<IngredientModel>();

        public IAsyncRelayCommand AddIngredientCommand => new AsyncRelayCommand(AddIngredientAsync);
        public IAsyncRelayCommand<IngredientModel> RemoveIngredientCommand => new AsyncRelayCommand<IngredientModel>(RemoveIngredientAsync);

        private async Task AddIngredientAsync()
        {
            Ingredients.Add(new IngredientModel());
            await Task.CompletedTask; // Simulating async operation if needed
        }

        public IngredientViewModel()
        {
            NavigateToDirections = new AsyncRelayCommand(NavigateToRecipeDirections);
        }

        private async Task RemoveIngredientAsync(IngredientModel ingredient)
        {
            Ingredients.Remove(ingredient);
            await Task.CompletedTask; // Simulating async operation if needed
        }

        [RelayCommand]
        public async Task NavigateToRecipeDirections()
        {
            await Shell.Current.GoToAsync("AddRecipeDirections");
        }
    }
}
