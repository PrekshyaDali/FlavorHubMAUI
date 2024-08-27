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
    public class CommentRepository : ICommentsRepository
    {
        private readonly SQLiteAsyncConnection _Database;

        public CommentRepository(SQLiteAsyncConnection database)
        {
            _Database = database;
        }
        public async Task AddCommentAsync(Comments comments)
        {
            try
            {
                await _Database.InsertAsync(comments);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }

        public Task DeleteCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public async Task<Comments> GetCommentByIdAsync(Guid commentId)
        {
            try
            {
                return await _Database.Table<Comments>().Where(c => c.CommentId == commentId).FirstOrDefaultAsync();
            }
            catch (Exception ex) 
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<Comments>> GetCommentsByRecipeIdAsync(Guid recipeId)
        {
            try
            {
                return await _Database.Table<Comments>().Where(c => c.RecipeId == recipeId).ToListAsync();
            }
            catch (Exception ex) 
            { 
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<Comments>> GetCommentsByUserIdAsync(Guid userId)
        {
            try
            {
                return await _Database.Table<Comments>().Where(c => c.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public Task UpdateCommentAsync(Comments comments)
        {
            throw new NotImplementedException();
        }
    }
}
