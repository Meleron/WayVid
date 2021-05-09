using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Model;

namespace WayVid.Infrastructure.Interfaces.Service
{
    public interface IUserService : ICrudGenericService<User, UserModel, ApiDbContext>
    {
        public Task<UserModel> GetUserByPrincipalAsync(ClaimsPrincipal principal);
        public Task DeleteAllUsersAsync();
    }
}
