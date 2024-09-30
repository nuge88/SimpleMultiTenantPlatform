using DocumentService.Domain.Entities;

namespace DocumentService.Domain.Interfaces
{
    public interface IAuditRepository
    {
        Task AddAuditAsync(Audit audit);
        Task<IEnumerable<Audit>> GetAuditsByEntityIdAsync(int entityId, string entityType);
        Task<IEnumerable<Audit>> GetAuditsByUserIdAsync(int userId);
    }
}