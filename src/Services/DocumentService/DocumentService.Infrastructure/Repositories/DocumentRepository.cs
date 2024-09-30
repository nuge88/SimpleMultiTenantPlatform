using DocumentService.Domain.Entities;
using DocumentService.Domain.Interfaces;
using DocumentService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DocumentService.Infrastructure.Repositories
{
    public class DocumentRepository(DocumentDbContext context) : IDocumentRepository
    {
        public async Task<Document?> GetByIdAsync(int id, int userId, int? organizationId)
        {
            return await context.Documents
                .Where(d => d.Id == id && (d.UserId == userId || d.OrganizationId == organizationId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Document>> GetAllByUserAsync(int userId)
        {
            return await context.Documents
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetAllByOrganizationAsync(int organizationId)
        {
            return await context.Documents
                .Where(d => d.OrganizationId == organizationId)
                .ToListAsync();
        }

        public async Task AddAsync(Document document)
        {
            await context.Documents.AddAsync(document);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document document)
        {
            context.Documents.Update(document);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Document document)
        {
            context.Documents.Remove(document);
            await context.SaveChangesAsync();
        }
    }
}