using Microsoft.EntityFrameworkCore;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Data.Contexts
{
    public class SystemContext : DbContext
    {
        public SystemContext() { }

        public SystemContext(DbContextOptions<SystemContext> options) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Resolve o mapeametno no DbContext
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
