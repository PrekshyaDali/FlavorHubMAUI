using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        Task<Comments> GetCommentByIdAsync(Guid commentId);
        Task<IEnumerable<Comments>> GetCommentsByRecipeIdAsync(Guid recipeId);
        Task<IEnumerable<Comments>> GetCommentsByUserIdAsync(Guid userId);
        Task AddCommentAsync(Comments comments);
        Task UpdateCommentAsync(Comments comments);
        Task DeleteCommentAsync(Guid commentId);
    }
}
