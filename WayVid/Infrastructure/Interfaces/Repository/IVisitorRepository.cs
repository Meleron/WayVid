using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Infrastructure.Interfaces.Repository
{
    public interface IVisitorRepository : IRepositoryGeneric<Visitor, ApiDbContext>
    {
        public Task<Visitor> GetVisitorByUserID(Guid UserID);
    }
}
