using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Enum;

namespace WayVid.Infrastructure.Interfaces.Entity
{
    public interface IUser
    {
        public Guid Id { get; set; }
        public Guid? VisitorID { get; set; }
        public Visitor Visitor { get; set; }
    }
}
