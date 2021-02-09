using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Enum;

namespace WayVid.Database.Model
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        public RoleType UserRole { get; set; }
    }
}
