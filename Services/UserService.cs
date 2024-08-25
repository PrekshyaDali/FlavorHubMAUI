using FlavorHub.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Services
{
    public class UserService : IUserService
    {
        private const string UserIdKey = "UserId";

        public async Task StoreUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("Invalid UserId", nameof(userId));
            }
            await SecureStorage.Default.SetAsync(UserIdKey, userId.ToString());
           
        }
        public async Task<Guid?> GetUserIdAsync()
        {
            var userIdString = await SecureStorage.Default.GetAsync(UserIdKey);
            if (Guid.TryParse(userIdString, out var userId))
            {
                return userId;
            }
            return null;
        }
    }
}
