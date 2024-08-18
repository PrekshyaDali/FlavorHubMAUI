using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlavorHub.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel
{
    public partial class AddRecipeViewModel: ObservableObject
    {
        [ObservableProperty]
        private TimeSpan _PreparationTime;
        public ICommand NavigateToAddRecipeInformation {  get; set; }  
        public ICommand NavigateToIngredientsPage { get; set; }
        public ICommand NavigateToDirections {  get; set; }
        public ICommand ServingSizeIncrease { get; set; }
        public ICommand ServingSizeDecrease { get; set; }
        public AddRecipeViewModel()
        {
            Console.WriteLine("I am here");
            NavigateToAddRecipeInformation = new AsyncRelayCommand(NavigateRecipeInformation);
            NavigateToIngredientsPage = new AsyncRelayCommand(NavigateRecipeIngredient);
            NavigateToDirections = new AsyncRelayCommand(NavigateToRecipeDirections);
        }
        [RelayCommand]
        public async Task NavigateRecipeInformation()
        {
            await Shell.Current.GoToAsync("AddRecipeInformation");
        }

        [RelayCommand]
        public async Task NavigateRecipeIngredient()
        {
            await Shell.Current.GoToAsync("AddRecipeIngredient");
        }
        [RelayCommand]
        public async Task NavigateToRecipeDirections()
        {
            await Shell.Current.GoToAsync("AddRecipeDirections");
        }



    }
}
