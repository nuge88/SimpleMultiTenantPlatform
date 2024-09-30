using OrganizationService.Domain.Entities;

namespace OrganizationService.Domain.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization?> GetByIdAsync(int id);
        Task<IEnumerable<Organization>> GetAllAsync();
        Task AddAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(Organization organization);
    }
}