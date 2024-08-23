using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private int _CookingTime; // Changed to int

        [ObservableProperty]
        private int _Servings = 1;

        [ObservableProperty]
        private string? _DifficultyLevel;

        [ObservableProperty]
        private string? _HoursPicker;

        [ObservableProperty]
        private string? _MinutesPicker;
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
