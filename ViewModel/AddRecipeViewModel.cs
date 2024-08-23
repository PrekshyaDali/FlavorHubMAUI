using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Models.SQLiteModels;
using FlavorHub.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class AddRecipeViewModel : ObservableObject
    {
        [ObservableProperty]
        private TimeSpan _PreparationTime;
        public ICommand NavigateToAddRecipeInformation { get; set; }


        [ObservableProperty]
        private string? _Title;

        [ObservableProperty]
        private string? _Description;

        public AddRecipeViewModel()
        {
            Console.WriteLine("I am here");
            NavigateToAddRecipeInformation = new AsyncRelayCommand(NavigateRecipeInformation);
        }
        [RelayCommand]
        public async Task NavigateRecipeInformation()
        {
            try
            {
                var recipe = new Recipe
                {
                    Title = Title,
                    Description = Description,
                };

                await Shell.Current.GoToAsync("AddRecipeInformation", new Dictionary<string, object>
                {
                    {"RecipeData", recipe }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

        }
    }
}
