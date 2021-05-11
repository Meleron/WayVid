using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Repository;

namespace WayVid.Database.Repository
{
    public class VisitLogItemRepository : RepositoryGeneric<VisitLogItem, ApiDbContext>, IVisitLogItemRepository
    {

        public VisitLogItemRepository(ApiDbContext context) : base(context)
        { }

        public override async Task<VisitLogItem> GetAsync(Guid ID)
        {
            return await context.VisitLogItemList.Include(e => e.Visitor).Include(e => e.Establishment).AsNoTracking().FirstOrDefaultAsync(e => e.ID == ID);
        }

        public async Task<VisitLogItem> GetLastLogByVisitorID(Guid VisitorID)
        {
            return await context.VisitLogItemList.Include(e => e.Visitor).Include(e => e.Establishment).OrderBy(e => e.CreatedOn).AsNoTracking().LastOrDefaultAsync(e => e.VisitorID == VisitorID);
        }
    }
}
