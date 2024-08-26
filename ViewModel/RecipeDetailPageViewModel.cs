using CommunityToolkit.Mvvm.ComponentModel;
using FlavorHub.ViewModel.RecipeFormViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.ViewModel
{
    public partial class RecipeDetailPageViewModel : ObservableObject, IQueryAttributable 
    {

        [ObservableProperty]
        private RecipeViewModel _SelectedRecipe;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("SelectedRecipe"))
            {
                SelectedRecipe = query["SelectedRecipe"] as RecipeViewModel;
                Console.WriteLine(SelectedRecipe.ImageUrls);
                Console.WriteLine(SelectedRecipe.Ingredients);
                // Debug output
                Console.WriteLine("Ingredients Count: " + SelectedRecipe.Ingredients.Count);
            }
        }
    }
}
