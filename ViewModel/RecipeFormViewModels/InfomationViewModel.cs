﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FlavorHub.Models;
using FlavorHub.Models.SQLiteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public partial class InfomationViewModel : ObservableObject, IQueryAttributable
    {

        [ObservableProperty]
        private string? _Title;

        [ObservableProperty]
        private string? _Description;

        [ObservableProperty]
        private int _CookingTime; 

        [ObservableProperty]
        private int _Servings = 1;

        [ObservableProperty]
        private string? _DifficultyLevel;

        [ObservableProperty]
        private string? _HoursPicker;

        [ObservableProperty]
        private string? _MinutesPicker;

        [ObservableProperty]
        private string? _HoursPickerErrorMessage;

        [ObservableProperty]
        private string? _MinutesPickerErrorMessage;

        [ObservableProperty]
        private bool _IsButtonEnabled;

        public ICommand ValidateCommand { get; set; }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("RecipeData"))
            {
                var recipe = query["RecipeData"] as Recipe;
                if (recipe != null)
                {
                    Title = recipe.Title;
                    Description = recipe.Description;
                }
            }
        }

        public ICommand ServingSizeIncrease { get; set; }
        public ICommand ServingSizeDecrease { get; set; }
        public ICommand NavigateToIngredientsPage { get; set; }

        public InfomationViewModel()
        {
           
            NavigateToIngredientsPage = new AsyncRelayCommand(NavigateRecipeIngredient);
            ServingSizeIncrease = new RelayCommand(IncreaseServingSize);
            ServingSizeDecrease = new RelayCommand(DecreaseServingSize);
            PropertyChanged += OnPropertyChanged;
            ValidateCommand = new RelayCommand(ValidateInputs);
            WeakReferenceMessenger.Default.Register<ClearDataMessage>(this, (r, message) =>
            {
                ClearData();
            });
        }
        public void ValidateInputs()
        {
            bool isHoursValid = int.TryParse(HoursPicker, out var hours) && hours >= 0;
            bool isMinutesValid = int.TryParse(MinutesPicker, out var minutes) && minutes >= 0 && minutes <= 60;
            HoursPickerErrorMessage = isHoursValid ? string.Empty : "Hours cannot be negative or non-numeric.";
            MinutesPickerErrorMessage = isMinutesValid ? string.Empty : "Minutes must be between 0 and 60, and numeric.";

            IsButtonEnabled = isHoursValid && isMinutesValid;
        }
        private void ClearData()
        {
            Title = null;
            Description = null;
            CookingTime = 0;
            Servings = 0;
            DifficultyLevel = null;
        }

        private void IncreaseServingSize()
        {
            Servings++;
        }

        private void DecreaseServingSize()
        {
            if (Servings > 1)
            {
                Servings--;
            }
        }
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HoursPicker) || e.PropertyName == nameof(MinutesPicker))
            {
                UpdateCookingTime();
            }
        }

        private void UpdateCookingTime()
        {
            int hours = 0;
            int minutes = 0;

            if (int.TryParse(HoursPicker, out hours))
            {
            }

            if (int.TryParse(MinutesPicker, out minutes))
            {
            }

            CookingTime = (hours * 60) + minutes; 
        }

        [RelayCommand]
        public async Task NavigateRecipeIngredient()
        {
            try
            {
                var recipe = new Recipe
                {
                    Title = Title,
                    Description = Description,
                    CookingTime = CookingTime, 
                    Servings = Servings,
                    DifficultyLevel = DifficultyLevel
                };
                await Shell.Current.GoToAsync("AddRecipeIngredient", new Dictionary<string, object>
                {
                    { "RecipeData", recipe }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
