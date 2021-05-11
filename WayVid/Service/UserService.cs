using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Database.Repository;
using WayVid.Infrastructure.Enum;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Service
{
    public class UserService : CrudGenericService<User, UserModel, ApiDbContext>, IUserService
    {

        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IRepositoryGeneric<User, ApiDbContext> repository;
        private readonly IOwnerService ownerService;

        public UserService(UserManager<User> userManager,
            IMapper mapper,
            IRepositoryGeneric<User, ApiDbContext> repository,
            IOwnerService ownerService)
            : base(repository, mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.repository = repository;
            this.ownerService = ownerService;
        }

        public async Task<ServiceCrudResponse<UserModel>> GetUserModelByPrincipalAsync(ClaimsPrincipal principal)
        {
            Claim subjectClaim = principal.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectConstants.Claims.Subject);
            if (subjectClaim == null || subjectClaim.Value == "")
                return ErrorResponse("Subject claim not found");
            User user = await repository.GetAsync(Guid.Parse(subjectClaim.Value));
            IList<string> roles = await userManager.GetRolesAsync(user);
            UserModel userToReturn = mapper.Map<UserModel>(user);

            userToReturn.RoleList = roles.Select(role => (RoleType)Enum.Parse(typeof(RoleType), role)).ToList();
            return SuccessResponse(userToReturn);
        }

        public async Task<ServiceCrudResponse<User>> GetEntityUserByPrincipalAsync(ClaimsPrincipal principal)
        {
            Claim subjectClaim = principal.Claims.FirstOrDefault(claim => claim.Type == OpenIdConnectConstants.Claims.Subject);
            if (subjectClaim == null || subjectClaim.Value == "")
                return null;
            //User user = await repository.GetAsync(Guid.Parse(subjectClaim.Value));
            User user = await userManager.GetUserAsync(principal);
            if(user == null)
                return new ServiceCrudResponse<User>(null, false, "User not found");
            return new ServiceCrudResponse<User>(user, true, "");
        }

        public async Task DeleteAllUsersAsync()
        {
            foreach (User user in userManager.Users.ToList())
            {
                await userManager.DeleteAsync(user);
            }
        }

        public async Task<bool> IsInRoleAsync(UserModel userModel, string role)
        {
            User user = mapper.Map<User>(userModel);
            return await userManager.IsInRoleAsync(user, role);
        }

        public async Task<ServiceCrudResponse<UserModel>> RequestOwnerRole(User user)
        {
            if (await userManager.IsInRoleAsync(user, RoleType.Owner.ToString()))
                return SuccessResponse(mapper.Map<UserModel>(user));
            await userManager.AddToRoleAsync(user, RoleType.Owner.ToString());
            OwnerModel owner = (await ownerService.InsertAsync(new OwnerModel { ID = Guid.NewGuid(), UserID = user.Id })).Model;
            user.OwnerID = owner.ID;
            user = await repository.UpdateAsync(user);
            if (user != null)
                return SuccessResponse(mapper.Map<UserModel>(user));
            return ErrorResponse("Error updating user");
        }
    }
}
