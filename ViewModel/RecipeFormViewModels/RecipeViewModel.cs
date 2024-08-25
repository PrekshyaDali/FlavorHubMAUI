using CommunityToolkit.Mvvm.ComponentModel;
using FlavorHub.Models.SQLiteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel.RecipeFormViewModels
{
    public class RecipeViewModel: ObservableObject
    {
        private readonly Recipe _Recipe;
        public RecipeViewModel(Recipe recipe)
        {
            _Recipe = recipe;
        }
        public string Title => _Recipe.Title;

        public string Description => _Recipe.Description;

        public int CookingTime => _Recipe.CookingTime;

        public string DifficultyLevel => _Recipe.DifficultyLevel;

        public int Servings => _Recipe.Servings;

        public string FirstImageUrl => _Recipe.ImageUrls.FirstOrDefault() ?? "dotnet_bot.png";
    }
}
