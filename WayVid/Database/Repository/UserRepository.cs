using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;
using WayVid.Infrastructure.Interfaces.Core;

namespace WayVid.Database.Repository
{
    public class UserRepository : RepositoryGeneric<User, ApiDbContext>
    {

        private readonly ApiDbContext context;

        public UserRepository(ApiDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public override async Task<User> GetAsync(Guid ID)
        {
            User user = await context.UserList.AsNoTracking().Include(e => e.Owner).Include(e => e.Visitor).AsNoTracking().FirstOrDefaultAsync(e => e.Id == ID);
            return user;
        }
    }
}
