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
    public class VisitorRepository : RepositoryGeneric<Visitor, ApiDbContext>, IVisitorRepository
    {

        private readonly ApiDbContext context;
        public VisitorRepository(ApiDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<Visitor> GetAsync(Guid ID)
        {
            return await context.Set<Visitor>().Include(e => e.User).FirstOrDefaultAsync(e => e.ID == ID);
        }

        public async Task<Visitor> GetVisitorByUserID(Guid UserID)
        {
            return await context.Set<Visitor>().Include(e => e.User).FirstOrDefaultAsync(e => e.UserID == UserID);
        }
    }
}
