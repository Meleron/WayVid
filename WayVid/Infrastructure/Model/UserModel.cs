using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Entity;

namespace WayVid.Infrastructure.Model
{
    public class UserModel : IUser
    {
        public Guid Id { get; set; }
        public Guid? VisitorID { get; set; }
        public Visitor Visitor { get; set; }
    }
}
