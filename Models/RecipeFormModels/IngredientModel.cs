using CommunityToolkit.Mvvm.ComponentModel;

namespace FlavorHub.Models.RecipeFormModels
{
    public partial class IngredientModel : ObservableObject
    {
        [ObservableProperty]
        private string? ingredientName;

        [ObservableProperty]
        private string? measurement;
    }
}
