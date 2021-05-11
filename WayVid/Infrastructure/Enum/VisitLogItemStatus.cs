using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayVid.Infrastructure.Enum
{

    [Flags]
    public enum VisitLogItemStatus
    {
        Active = 1,
        Closed = 1 << 1,
        ForciblyClosed = 1 << 2
    }
}
