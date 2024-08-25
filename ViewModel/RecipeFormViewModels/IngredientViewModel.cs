using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FlavorHub.Models;
using FlavorHub.Models.RecipeFormModels;
using FlavorHub.Models.SQLiteModels;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public partial class IngredientViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private string? _Title;

        [ObservableProperty]
        private string? _Description;

        [ObservableProperty]
        private int _CookingTime;

        [ObservableProperty]
        private int _Servings;

        [ObservableProperty]
        private string? _DifficultyLevel;
        [ObservableProperty]
        private string? _IngredientsJson;

        [ObservableProperty]
        private ObservableCollection<IngredientModel> _Ingredients = new ObservableCollection<IngredientModel>();

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("RecipeData"))
            {
                var recipe = query["RecipeData"] as Recipe;
                if (recipe != null)
                {
                    Title = recipe.Title;
                    Description = recipe.Description;
                    CookingTime = recipe.CookingTime;
                    Servings = recipe.Servings;
                    DifficultyLevel = recipe.DifficultyLevel;
                    IngredientsJson = recipe.IngredientsJson;

                    // Load ingredients from JSON
                    LoadIngredientsFromJson(IngredientsJson);
                }
            }
        }
     

        public IAsyncRelayCommand AddIngredientCommand => new AsyncRelayCommand(AddIngredientAsync);
        public IAsyncRelayCommand NavigateToDirections => new AsyncRelayCommand(NavigateToRecipeDirections);
        public IAsyncRelayCommand<IngredientModel> RemoveIngredientCommand => new AsyncRelayCommand<IngredientModel>(RemoveIngredientAsync);

        public IngredientViewModel()
        {
            WeakReferenceMessenger.Default.Register<ClearDataMessage>(this, (r, message) =>
            {
                ClearData();
            });
        }

        public void ClearData()
        {
            Title = null;
            Description = null;
            CookingTime = 0;
            Servings = 0;
            DifficultyLevel = null;
            IngredientsJson = null;
            Ingredients.Clear();
        }

        private async Task AddIngredientAsync()
        {
            Ingredients.Add(new IngredientModel());
            await Task.CompletedTask;
        }

        private async Task RemoveIngredientAsync(IngredientModel ingredient)
        {
            Ingredients.Remove(ingredient);
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task NavigateToRecipeDirections()
        {
            UpdateIngredientsJson(); 
            var recipe = new Recipe
            {
                Title = Title,
                Description = Description,
                CookingTime = CookingTime,
                Servings = Servings,
                DifficultyLevel = DifficultyLevel,
                IngredientsJson = IngredientsJson 
            };

            Console.WriteLine("Recipe Ingredients JSON: " + recipe.IngredientsJson);

             await Shell.Current.GoToAsync("AddRecipeDirections", new Dictionary<string, object>
            {
                { "RecipeData", recipe }
            });
        }

        public void LoadIngredientsFromJson(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                var ingredients = JsonConvert.DeserializeObject<ObservableCollection<IngredientModel>>(json);
                if (ingredients != null)
                {
                    Ingredients = ingredients;
                }
            }
        }

        private void UpdateIngredientsJson()
        {
            IngredientsJson = JsonConvert.SerializeObject(Ingredients);
        }
    }
}
