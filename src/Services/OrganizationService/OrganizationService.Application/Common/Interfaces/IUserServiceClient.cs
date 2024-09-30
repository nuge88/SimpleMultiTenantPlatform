using Shared.Contracts.UserContracts;

namespace OrganizationService.Application.Common.Interfaces
{
    public interface IUserServiceClient
    {
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<bool> AssignUserToOrganizationAsync(int userId, int organizationId);
    }
}