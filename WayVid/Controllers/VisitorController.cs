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

namespace WayVid.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class VisitorController : ControllerBase
    {
        private RoleManager<Role> roleManager;
        private UserManager<User> userManager;

        public VisitorController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpPost("ownerRoleRequest")]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> OwnerRoleRequest()
        {
            User user = await userManager.GetUserAsync(User);
            if (!await userManager.IsInRoleAsync(user, RoleType.Owner.ToString()))
            {
                await userManager.AddToRoleAsync(user, RoleType.Owner.ToString());
            }
            return Ok();
        }
    }
}
