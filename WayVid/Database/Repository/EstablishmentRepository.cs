using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Model;

namespace WayVid.Database.Repository
{
    public class EstablishmentRepository : RepositoryGeneric<Establishment, ApiDbContext>
    {

        private readonly ApiDbContext context;

        public EstablishmentRepository(ApiDbContext context) : base(context)
        {
            this.context = context;
        }
        public override async Task<Establishment> GetAsync(Guid ID)
        {
            return await context.EstablishmentList.Include(e => e.OwnerEstablishmentList).ThenInclude(ownEstLst => ownEstLst.Owner).FirstOrDefaultAsync(e => e.ID == ID);
        }
    }
}
