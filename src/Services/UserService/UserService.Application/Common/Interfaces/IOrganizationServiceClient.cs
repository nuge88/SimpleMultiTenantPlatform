namespace UserService.Application.Common.Interfaces
{
    public interface IOrganizationServiceClient
    {
        Task<bool> OrganizationExistsAsync(int organizationId);
    }
}