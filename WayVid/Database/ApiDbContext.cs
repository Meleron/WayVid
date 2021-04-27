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
            user.HasKey(entity => entity.Id);
            user.HasOne(entity => entity.Visitor).WithOne(destionation => destionation.User).HasForeignKey<Visitor>(d => d.UserID);
            EntityTypeBuilder<Visitor> visitor = builder.Entity<Visitor>();
            visitor.HasKey(e => e.ID);
            visitor.HasOne(e => e.User).WithOne(d => d.Visitor).HasForeignKey<User>(d => d.VisitorID);
            base.OnModelCreating(builder);
        }
    }
}
