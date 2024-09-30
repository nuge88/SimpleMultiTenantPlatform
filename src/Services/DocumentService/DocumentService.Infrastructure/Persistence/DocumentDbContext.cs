using Microsoft.EntityFrameworkCore;
using DocumentService.Domain.Entities;

namespace DocumentService.Infrastructure.Persistence
{
    public class DocumentDbContext(DbContextOptions<DocumentDbContext> options) : DbContext(options)
    {
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<Audit> Audits { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}