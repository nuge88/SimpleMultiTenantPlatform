using DocumentService.Domain.Entities;
using DocumentService.Domain.Interfaces;
using DocumentService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DocumentService.Infrastructure.Repositories
{
    public class AuditRepository(DocumentDbContext context) : IAuditRepository
    {
        public async Task AddAuditAsync(Audit audit)
        {
            await context.Audits.AddAsync(audit);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Audit>> GetAuditsByEntityIdAsync(int entityId, string entityType)
        {
            return await context.Audits
                .Where(a => a.EntityId == entityId && a.EntityType == entityType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Audit>> GetAuditsByUserIdAsync(int userId)
        {
            return await context.Audits
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
    }
}
