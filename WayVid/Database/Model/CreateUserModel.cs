using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Enum;

namespace WayVid.Database.Model
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType UserRole { get; set; }
    }
}
