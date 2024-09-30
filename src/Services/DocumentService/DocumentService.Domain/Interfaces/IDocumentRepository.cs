using DocumentService.Domain.Entities;

namespace DocumentService.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(int id, int userId, int? organizationId);
        Task<IEnumerable<Document>> GetAllByUserAsync(int userId);
        Task<IEnumerable<Document>> GetAllByOrganizationAsync(int organizationId);
        Task AddAsync(Document document);
        Task UpdateAsync(Document document);
        Task DeleteAsync(Document document);
    }
}