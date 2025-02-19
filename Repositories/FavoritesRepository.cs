﻿using FlavorHub.Models.SQLiteModels;
using FlavorHub.Repositories.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly SQLiteAsyncConnection _Database;
        public FavoritesRepository(SQLiteAsyncConnection database)
        {
            _Database = database;
        }
        public async Task AddFavoritesAsync(Favorites favorites)
        {
            try
            {
                await _Database.InsertAsync(favorites);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        public async Task DeleteFavoritesByIdAsync(Favorites favorites)
        {
            try
            {
                await _Database.DeleteAsync(favorites);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        public async Task<Favorites> GetFavoriteByRecipeAndUserAsync(Guid recipeId, Guid userId)
        {
            try
            {
                var favorite = await _Database.Table<Favorites>()
                    .Where(f => f.RecipeId == recipeId && f.UserId == userId)
                    .FirstOrDefaultAsync();

                return favorite;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving favorite: {ex.Message}");
                return null;
            }

        }

        public async Task<int> GetFavoriteCountByRecipeIdAsync(Guid recipeId)
        {
            try
            {
                var count = await _Database.Table<Favorites>().CountAsync(f => f.RecipeId == recipeId);
                return count;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public async Task<Favorites> GetFavoritesById(Guid favoritesId)
        {
            try
            {
                return await _Database.Table<Favorites>().Where(favorites => favorites.FavoritesId == favoritesId).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;

            }
        }

        public async Task<IEnumerable<Favorites>> GetFavoritesByRecipeId(Guid recipeId)
        {
            try
            {
                return await _Database.Table<Favorites>().Where(favorites => favorites.RecipeId == recipeId).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;

            }

        }

        public async Task<IEnumerable<Favorites>> GetFavoritesByUserId(Guid userId)
        {
            try
            {
                Console.WriteLine($"Fetching favorites for UserId: {userId}");
                return await _Database.Table<Favorites>().Where(favorites => favorites.UserId == userId).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;

            }
        }

        public async Task UpdateCommentAsync(Favorites favorites)
        {
            try
            {
                await _Database.UpdateAsync(favorites);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
