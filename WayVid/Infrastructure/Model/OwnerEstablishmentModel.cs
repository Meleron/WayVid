using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Entity;

namespace WayVid.Infrastructure.Model
{
    public class OwnerEstablishmentModel : IOwnerEstablishment
    {
        public Guid ID { get; set; }
        public Guid OwnerID { get; set; }
        public OwnerModel Owner { get; set; }
        public Guid EstablishmentID { get; set; }
        public EstablishmentModel Establishment { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
