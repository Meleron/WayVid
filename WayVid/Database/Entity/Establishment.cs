using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Interfaces.Entity;

namespace WayVid.Database.Entity
{
    public class Establishment : IEstablishment
    {
        public Guid ID { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public List<OwnerEstablishment> OwnerEstablishmentList { get; set; }
        public List<VisitLogItem> VisitLogItemList { get; set; }
        public string EstablishmentName { get; set; }
        public string MenuUrl { get; set; }
    }
}
