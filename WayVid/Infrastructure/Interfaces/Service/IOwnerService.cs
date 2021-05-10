using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Core;
using WayVid.Infrastructure.Model;

namespace WayVid.Infrastructure.Interfaces.Service
{
    public interface IOwnerService : ICrudGenericService<Owner, OwnerModel, ApiDbContext>
    {

    }
}
