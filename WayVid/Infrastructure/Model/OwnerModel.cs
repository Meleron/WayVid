using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Entity;

namespace WayVid.Infrastructure.Model
{
    public class OwnerModel : IOwner
    {
        public Guid ID { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public Guid UserID { get; set; }
        public UserModel User { get; set; }
        public List<OwnerEstablishmentModel> OwnerEstablishmentList { get; set; }
    }
}
