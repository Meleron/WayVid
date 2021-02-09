using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Enum;

namespace WayVid.Service
{
    public class RoleService
    {

        private readonly RoleManager<Role> roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public void InitializeRoles()
        {
            List<string> allRoles = new List<string>(Enum.GetNames(typeof(RoleType)));
            List<string> existingRoles = roleManager.Roles.Select(role => role.Name).ToList();
            foreach (string missingRole in allRoles.Except(existingRoles))
            {
                try
                {
                    IdentityResult res = roleManager.CreateAsync(new Role { Name = missingRole }).Result;
                } catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
