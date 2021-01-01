using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Entity;

namespace WayVid.Database
{
    public class ApiDbContext : IdentityDbContext<User, Role, Guid>
    {

        DbSet<User> userList { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EntityTypeBuilder<User> user = builder.Entity<User>();
            base.OnModelCreating(builder);
        }
    }
}
