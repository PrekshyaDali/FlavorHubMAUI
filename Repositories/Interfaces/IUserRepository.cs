using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlavorHub.Models.SQLiteModels;

namespace FlavorHub.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAysnc(User user);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByFirebaseUidAsync(string firebaseUid);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        void ClearCachedUser();
    }
}
