using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Model;

namespace WayVid.Infrastructure.Interfaces.Service
{
    public interface IUserService : ICrudGenericService<User, UserModel, ApiDbContext>
    {
        public Task<ServiceCrudResponse<UserModel>> GetUserModelByPrincipalAsync(ClaimsPrincipal principal);
        public Task<bool> IsInRoleAsync(UserModel userModel, string role);
        public Task DeleteAllUsersAsync();
        public Task<ServiceCrudResponse<UserModel>> RequestOwnerRole(User user);
        public Task<ServiceCrudResponse<User>> GetEntityUserByPrincipalAsync(ClaimsPrincipal principal);
    }
}
