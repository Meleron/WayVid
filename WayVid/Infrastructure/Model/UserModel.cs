using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Enum;
using WayVid.Infrastructure.Interfaces.Entity;

namespace WayVid.Infrastructure.Model
{
    public class UserModel : IUser
    {
        public Guid Id { get; set; }
        public List<RoleType> RoleList { get; set; }
        public Guid? VisitorID { get; set; }
        public VisitorModel Visitor { get; set; }
        public Guid? OwnerID { get; set; }
        public OwnerModel Owner { get; set; }
        public string UserName { get; set; }
    }
}
