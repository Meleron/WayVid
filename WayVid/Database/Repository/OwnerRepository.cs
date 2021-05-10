using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;

namespace WayVid.Database.Repository
{
    public class OwnerRepository : RepositoryGeneric<Owner, ApiDbContext>
    {

        private readonly ApiDbContext context;

        public OwnerRepository(ApiDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public override async Task<Owner> GetAsync(Guid ID)
        {
            Owner owner = await context.OwnerList.AsNoTracking().Include(e => e.User).FirstOrDefaultAsync(e => e.ID == ID);
            return owner;
        }
    }
}
