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
    public class VisitLogItemModel : IVisitLogItem
    {
        public Guid ID { get; set; }
        public VisitLogItemType ActionType { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public DateTimeOffset? ExitedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public Guid VisitorID { get; set; }
        public Visitor Visitor { get; set; }
        public Guid EstablishmentID { get; set; }
        public Establishment Establishment { get; set; }
    }
}
