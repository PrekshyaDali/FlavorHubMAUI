using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly SQLiteAsyncConnection _Database;
        public RecipeRepository()
        {
            _Database = _Database;
        }
        public Task<Recipe> AddRecipeAsync(Recipe recipe)
        {
     
            throw new NotImplementedException();
        }

        public Task DeleteRecipeAsync(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetRecipeByIdAsync(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> UpdateRecipeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> UpdateRecipeAsync(Guid recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
