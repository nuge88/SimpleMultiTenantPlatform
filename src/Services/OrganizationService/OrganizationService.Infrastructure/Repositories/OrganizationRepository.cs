using Microsoft.EntityFrameworkCore;
using OrganizationService.Domain.Entities;
using OrganizationService.Domain.Interfaces;
using OrganizationService.Infrastructure.Persistence;

namespace OrganizationService.Infrastructure.Repositories
{
    public class OrganizationRepository(OrganizationDbContext context) : IOrganizationRepository
    {
        public async Task<Organization?> GetByIdAsync(int id)
        {
            return await context.Organizations.FindAsync(id);
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            return await context.Organizations.ToListAsync();
        }

        public async Task AddAsync(Organization organization)
        {
            await context.Organizations.AddAsync(organization);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization organization)
        {
            context.Entry(organization).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Organization organization)
        {
            context.Organizations.Remove(organization);
            await context.SaveChangesAsync();
        }
    }
}