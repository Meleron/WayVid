using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
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

        public DbSet<User> UserList { get; set; }
        public DbSet<Visitor> VisitorList { get; set; }
        public DbSet<Owner> OwnerList { get; set; }
        public DbSet<Establishment> EstablishmentList { get; set; }
        public DbSet<OwnerEstablishment> OwnerEstablishmentList { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory =
    LoggerFactory.Create(builder => { builder.AddConsole(); });

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EntityTypeBuilder<User> user = builder.Entity<User>();
            user.ToTable("User");
            user.HasKey(entity => entity.Id);
            user.HasOne(entity => entity.Visitor).WithOne(destionation => destionation.User).HasForeignKey<Visitor>(d => d.UserID);
            user.HasOne(entity => entity.Owner).WithOne(destionation => destionation.User).HasForeignKey<Owner>(d => d.UserID);

            EntityTypeBuilder<Visitor> visitor = builder.Entity<Visitor>();
            visitor.ToTable("Visitor");
            visitor.HasKey(e => e.ID);
            visitor.HasOne(e => e.User).WithOne(d => d.Visitor).HasForeignKey<User>(d => d.VisitorID);

            EntityTypeBuilder<Owner> owner = builder.Entity<Owner>();
            owner.ToTable("Owner");
            owner.HasKey(e => e.ID);
            owner.HasOne(e => e.User).WithOne(d => d.Owner).HasForeignKey<User>(d => d.OwnerID);
            owner.HasMany(e => e.OwnerEstablishmentList).WithOne(d => d.Owner).HasForeignKey(d => d.OwnerID);

            EntityTypeBuilder<Establishment> establishment = builder.Entity<Establishment>();
            establishment.ToTable("Establishment");
            establishment.HasKey(e => e.ID);
            establishment.HasMany(e => e.OwnerEstablishmentList).WithOne(d => d.Establishment).HasForeignKey(d => d.EstablishmentID);

            EntityTypeBuilder<OwnerEstablishment> ownerEstablishment = builder.Entity<OwnerEstablishment>();
            ownerEstablishment.ToTable("OwnerEstablishment");
            ownerEstablishment.HasKey(e => e.ID);
            ownerEstablishment.HasOne(e => e.Establishment).WithMany(d => d.OwnerEstablishmentList).HasForeignKey(e => e.EstablishmentID);
            ownerEstablishment.HasOne(e => e.Owner).WithMany(d => d.OwnerEstablishmentList).HasForeignKey(e => e.OwnerID);

            base.OnModelCreating(builder);
        }
    }
}
