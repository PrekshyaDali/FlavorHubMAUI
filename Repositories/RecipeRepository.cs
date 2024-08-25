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
        public RecipeRepository(SQLiteAsyncConnection database)
        {
            _Database = database;
        }
        public async Task AddRecipeAsync(Recipe recipe)
        {
            try
            {
                await _Database.InsertAsync(recipe);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding recipe: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteRecipeAsync(Guid recipeId)
        {
            try
            {
                var recipe = await GetRecipeByIdAsync(recipeId);
                if (recipe != null)
                {
                    await _Database.DeleteAsync(recipe);
                }
            }
            catch (Exception ex) 
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            try
            {
                return await _Database.Table<Recipe>().ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new NotImplementedException();

            }
        }

        public async Task<Recipe> GetRecipeByIdAsync(Guid recipeId)
        {
            try
            {
                return await _Database.Table<Recipe>().Where(recipe => recipe.RecipeId == recipeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }


        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            try
            {
                await _Database.UpdateAsync(recipe);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

    }
}
