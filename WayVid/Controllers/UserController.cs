using AspNet.Security.OpenIdConnect.Primitives;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Infrastructure.Model;

namespace WayVid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet("GetUserInfoFromContext")]
        [Authorize]
        public async Task<IActionResult> GetUserInfoFromContext()
        {
            AuthenticateResult authRes = await HttpContext.AuthenticateAsync(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            if (authRes.Succeeded)
            {
                UserModel user = await userService.GetUserByPrincipalAsync(authRes.Principal);
                if(user != null)
                    return Ok(user);
            }
            return BadRequest("User not found");
        }

        [HttpGet("DeleteAllUser")]
        public async Task DeleteAllUser()
        {
            await userService.DeleteAllUsersAsync();
        }
    }
}
