using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Database.Model;
using WayVid.Enum;
using WayVid.Extras;

namespace WayVid.Service
{
    public class IdentityService
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;

        public IdentityService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> CreateUserAsync(CreateUserModel createModel)
        {
            //if (createModel.UserRole == RoleType.Admin)
            //    throw new Exception("IdentityService.Register: Wrong role type!");
            User newUser = new User { UserName = createModel.UserName };
            IdentityResult identityRes = await userManager.CreateAsync(newUser, createModel.Password);
            if (identityRes.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, createModel.UserRole.ToString());
                await signInManager.SignInAsync(newUser, false);
                return true;
            }
            throw new StatusCodeException(HttpStatusCode.Conflict, identityRes.Errors.ToList().First().Description);
            //return false;
        }

        public async Task<bool> SignInAsync(SignInModel signInModel)
        {
            SignInResult signInRes = await signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, signInModel.RememberMe, false);
            if (signInRes.Succeeded)
                return true;
            throw new StatusCodeException(HttpStatusCode.Unauthorized, "Invalid login attempt");
            //return false;
        }


    }
}
