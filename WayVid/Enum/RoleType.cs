using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Enum
{
    [Flags]
    public enum RoleType
    {
        Visitor = 1,
        Owner = 1 << 1,
        Admin = 1 << 2
    }
}
