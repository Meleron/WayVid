using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Extras;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Model;

namespace WayVid.Infrastructure.Interfaces.Service
{
    public interface IOwnerEstablishmentService : ICrudGenericService<OwnerEstablishment, OwnerEstablishmentModel, ApiDbContext>
    {
        public Task<ServiceCrudResponse<OwnerEstablishmentModel>> GetTopOwnerEstablishmentForOwner(Guid OwnerID);
    }
}
