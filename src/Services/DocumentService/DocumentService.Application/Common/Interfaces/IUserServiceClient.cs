using Shared.Contracts.UserContracts;

namespace DocumentService.Application.Common.Interfaces
{
    public interface IUserServiceClient
    {
        Task<UserDto?> GetUser(int userId);
    }
}