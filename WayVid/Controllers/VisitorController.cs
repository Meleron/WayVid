using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Enum;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Service;
using WayVid.Service;

namespace WayVid.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class VisitorController : ControllerBase
    {
        private RoleManager<Role> roleManager;
        private UserManager<User> userManager;
        private IUserService userService;

        public VisitorController(RoleManager<Role> roleManager, UserManager<User> userManager, IUserService userService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userService = userService;
        }


        // todo: move to userController
        [HttpPost("ownerRoleRequest")]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> OwnerRoleRequest()
        {
            ServiceCrudResponse<User> resp = await userService.GetEntityUserByPrincipalAsync(User);
            if (!resp.Success || resp.Model == null)
                return BadRequest(resp.Message);
            await userService.RequestOwnerRole(resp.Model);
            return Ok();
        }
    }
}
