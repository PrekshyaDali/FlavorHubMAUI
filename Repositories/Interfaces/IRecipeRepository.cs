using FlavorHub.Models.SQLiteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeByIdAsync(Guid recipeId);
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task AddRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(Guid recipeId);
        Task UpdateRecipeAsync(Recipe recipe);
        
        Task<IEnumerable<Recipe>> GetRecipeByUserIdAsync(Guid userId);
    }
}
