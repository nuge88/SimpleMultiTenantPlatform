using Microsoft.EntityFrameworkCore;
using OrganizationService.Domain.Entities;

namespace OrganizationService.Infrastructure.Persistence
{
    public class OrganizationDbContext(DbContextOptions<OrganizationDbContext> options) : DbContext(options)
    {
        public DbSet<Organization> Organizations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}