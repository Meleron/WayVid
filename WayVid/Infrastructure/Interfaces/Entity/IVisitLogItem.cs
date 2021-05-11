using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Enum;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Infrastructure.Interfaces.Entity
{
    public interface IVisitLogItem : IEntity
    {
        public DateTimeOffset? ExitedOn { get; set; }
        public VisitLogItemStatus Status { get; set; }
        public Guid VisitorID { get; set; }
        public Guid EstablishmentID { get; set; }
    }
}
