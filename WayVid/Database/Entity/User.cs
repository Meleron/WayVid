using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Enum;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Entity;

namespace WayVid.Database.Entity
{
    public class User : IdentityUser<Guid>, IUser, IModel
    {
        public Guid? VisitorID { get; set; }
        public Visitor Visitor { get; set; }
        public Guid? OwnerID { get; set; }
        public Owner Owner { get; set; }
    }
}
