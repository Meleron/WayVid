using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Database.Model;
using WayVid.Infrastructure.Enum;
using WayVid.Service;

namespace WayVid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {

        private readonly IdentityService identityService;
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public IdentityController(IdentityService identityService, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            this.identityService = identityService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserModel registerModel)
        {
            await identityService.CreateUserAsync(registerModel);
            return StatusCode(201);
        }

        [Authorize, HttpGet("~/api/test")]
        public IActionResult GetMessage()
        {
            return new JsonResult(new
            {
                Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                Name = User.Identity.Name
            });
        }

        [HttpGet("NoAuth")]
        public async Task<ActionResult<SignInModel>> NoAuth()
        {
            return new SignInModel { Password = "qwe", RememberMe = false, UserName = "asd" };
        }

        [HttpGet("WithAuth")]
        [Authorize(Roles = "Visitor")]
        public string WithAuth()
        {
            return "Success visitor!";
        }

        [HttpGet("Owner")]
        [Authorize(Roles = "Owner")]
        public string Owner()
        {
            return "Success owner!";
        }

        [HttpGet("Test")]
        public async Task<bool> Test()
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            IList<string> roles = await userManager.GetRolesAsync(user);
            //HttpContext.User;
            return true;
        }

    }
}
