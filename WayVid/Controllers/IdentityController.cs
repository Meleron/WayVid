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
using WayVid.Enum;
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
        public async Task<string> Register(CreateUserModel registerModel)
        {
            if ((await identityService.CreateUserAsync(registerModel)) == true)
                return "Success!";
            return "Fail!";
        }

        [HttpPost]
        public async Task<string> Login(SignInModel signInModel)
        {
            if ((await identityService.SignInAsync(signInModel)) == true)
                return "Success";
            return "Fail";
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
