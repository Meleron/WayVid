using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (!ModelState.IsValid)
                return BadRequest();
            if ((await identityService.CreateUserAsync(registerModel)) == true)
                return Content("Success!");
            return Content("Fail!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(SignInModel signInModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Where(v => v.Errors.Count > 0).ToList());
            if ((await identityService.SignInAsync(signInModel)) == true)
                return Content("Success!");
            return Content("Fail!");
        }

        [HttpGet("NoAuth")]
        public async Task<string> NoAuth()
        {
            return "Success no auth!";
        }

        [HttpGet("WithAuth")]
        [Authorize]
        public string WithAuth()
        {
            return "Success with auth!";
        }
    }
}
