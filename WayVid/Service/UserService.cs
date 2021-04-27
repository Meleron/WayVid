using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Enum;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Service
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserModel> GetUserByPrincipalAsync(ClaimsPrincipal principal)
        {
            User user = await userManager.GetUserAsync(principal);
            IList<string> roles = await userManager.GetRolesAsync(user);
            UserModel userToReturn = mapper.Map<UserModel>(user);
            userToReturn.Role = (RoleType) Enum.Parse(typeof(RoleType), roles.FirstOrDefault());
            return userToReturn;
        }
    }
}
