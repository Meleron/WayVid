using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
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

        public IdentityController(IdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserModel registerModel)
        {
            await identityService.CreateUserAsync(registerModel);
            return Ok("");
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
    }
}
