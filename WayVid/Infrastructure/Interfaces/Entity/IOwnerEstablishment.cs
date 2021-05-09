using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Infrastructure.Interfaces.Entity
{
    public interface IOwnerEstablishment : IEntity
    {
        public Guid OwnerID { get; set; }
        public Guid EstablishmentID { get; set; }
    }
}
