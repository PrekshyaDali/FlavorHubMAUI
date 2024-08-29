using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FlavorHub.Models;
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

        [ObservableProperty]
        private bool _IsButtonEnabled;

        [ObservableProperty]
        private string? _TitleErrorMessage;

        [ObservableProperty]
        private string? _DescriptionErrorMessage;
        public ICommand ValidateCommand {  get; set; }

        public AddRecipeViewModel()
        {
            ValidateCommand = new RelayCommand(ValidateInputs);
            NavigateToAddRecipeInformation = new AsyncRelayCommand(NavigateRecipeInformation);
            WeakReferenceMessenger.Default.Register<ClearDataMessage>(this, (r, message) =>
            {
                ClearData();
            });
        }

        private void ValidateInputs()
        {
            bool isTitleValid = !string.IsNullOrEmpty(Title);
            bool isDescriptionValid = !string.IsNullOrEmpty(Description) && Description.Split(' ').Length >= 10;

            TitleErrorMessage = isTitleValid ? string.Empty : "Title cannot be empty.";
            DescriptionErrorMessage = isDescriptionValid ? string.Empty : "Description must be at least 10 words.";

            IsButtonEnabled = isTitleValid && isDescriptionValid;
        }

        private void ClearData()
        {
            Title = null;
            Description = null;
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
