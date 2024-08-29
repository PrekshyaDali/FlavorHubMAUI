using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FlavorHub.Models;
using FlavorHub.Models.RecipeFormModels;
using FlavorHub.Models.SQLiteModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public partial class DirectionViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private ObservableCollection<DirectionModel> _Directions = new ObservableCollection<DirectionModel>();

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
        private string? _StepsJson;

        [ObservableProperty]
        private string _DirectionErrorMessage;

        // Commands
        public IAsyncRelayCommand AddDirectionCommand => new AsyncRelayCommand(AddDirectionsAsync);
        public IAsyncRelayCommand<DirectionModel> RemoveDirectionCommand => new AsyncRelayCommand<DirectionModel>(RemoveDirectionsAsync);
        public IAsyncRelayCommand NavigateToAddUploads => new AsyncRelayCommand(NavigateToAddUploadsPage);

        public DirectionViewModel()
        {
            WeakReferenceMessenger.Default.Register<ClearDataMessage>(this, (r, message) =>
            {
                Console.WriteLine("Clear data message recienved in direction");
                ClearData();
            });
        }

        private void ClearData() {
            Title = null;
            Description = null;
            CookingTime = 0;
            Servings = 0;
            DifficultyLevel = null;
            IngredientsJson = null;
            StepsJson = null;
            Directions.Clear();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
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
                        StepsJson = recipe.StepsJson;
                        LoadDirectionsFromJson(StepsJson);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // Methods
        private async Task AddDirectionsAsync()
        {
            _Directions.Add(new DirectionModel());
            await Task.CompletedTask;
        }

        private async Task RemoveDirectionsAsync(DirectionModel direction)
        {
            _Directions.Remove(direction);
            await Task.CompletedTask;
        }

        public async Task NavigateToAddUploadsPage()
        {
            try
            {
                UpdateDirectionsJson();
                var recipe = new Recipe
                {
                    Title = Title,
                    Description = Description,
                    CookingTime = CookingTime,
                    Servings = Servings,
                    DifficultyLevel = DifficultyLevel,
                    IngredientsJson = IngredientsJson,
                    StepsJson = StepsJson, 
                };

                // Passing data to the next page
                await Shell.Current.GoToAsync("AddUploads", new Dictionary<string, object>
                {
                    { "RecipeData", recipe }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void UpdateDirectionsJson()
        {
            StepsJson = JsonConvert.SerializeObject(Directions);
        }

       private void LoadDirectionsFromJson(string? json)
       {
          if (!string.IsNullOrEmpty(json))
          {
             var directions = JsonConvert.DeserializeObject<ObservableCollection<DirectionModel>>(json);
             if (directions != null)
             {
                Directions = directions;
             }
          }
       }
    }
}
