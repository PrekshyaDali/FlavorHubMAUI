using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Repositories.Interfaces
{
    public  interface IUserService
    {
        Task StoreUserIdAsync(Guid userId);
        Task<Guid?> GetUserIdAsync();
    }
}
