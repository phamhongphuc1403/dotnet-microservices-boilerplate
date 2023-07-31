using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext<UserEntity>
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<DealEntity> Deals { get; set; }
        public DbSet<DealProductEntity> DealsProducts { get; set; }
        public DbSet<LeadEntity> Leads { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}