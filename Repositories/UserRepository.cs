using FlavorHub.Data;
using FlavorHub.Models.SQLiteModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Repositories
{
    public class UserRepository: Interfaces.IUserRepository
    {
        private readonly SQLiteAsyncConnection _Database;
        private User _CachedUser;
        public UserRepository(AppDbContext dbContext)
        {
            _Database = dbContext.GetConnection();
        }

        public void ClearCachedUser()
        {
            _CachedUser = null;
        }

        public async Task CreateUserAysnc(User user)
        {
            try
            {
                await _Database.InsertAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _Database.Table<User>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }

        public async Task<User> GetUserByFirebaseUidAsync(string firebaseUid)
        {
            try
            {
                var user = await _Database.Table<User>().Where(u => u.FirebaseUID == firebaseUid).FirstOrDefaultAsync();
                _CachedUser = user;
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new NotImplementedException();
            }
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _Database.Table<User>().Where(u => u.UserId == userId).FirstOrDefaultAsync();
                _CachedUser = user;
                return user;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new NotImplementedException();
            }

        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                await _Database.UpdateAsync(user);
                _CachedUser = user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }
    }
}
