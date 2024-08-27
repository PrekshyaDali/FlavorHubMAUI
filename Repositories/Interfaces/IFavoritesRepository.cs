using FlavorHub.Models.SQLiteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        Task<Favorites> GetFavoritesById(Guid favoritesId);
        Task<IEnumerable<Favorites>> GetFavoritesByRecipeId(Guid recipeId);
        Task<IEnumerable<Favorites>> GetFavoritesByUserId(Guid userId);
        Task AddFavoritesAsync(Favorites favorites);
        Task UpdateCommentAsync(Favorites favorites);
        Task DeleteFavoritesByIdAsync(Guid favoritesId);
    }
}
