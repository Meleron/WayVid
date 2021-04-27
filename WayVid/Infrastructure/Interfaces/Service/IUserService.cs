using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Model;

namespace WayVid.Infrastructure.Interfaces.Service
{
    public interface IUserService
    {
        public Task<UserModel> GetUserByPrincipalAsync(ClaimsPrincipal principal);
    }
}
