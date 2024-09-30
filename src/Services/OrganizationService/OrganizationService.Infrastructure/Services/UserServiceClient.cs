using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using OrganizationService.Application.Common.Interfaces;
using Shared.Contracts.UserContracts;

namespace OrganizationService.Infrastructure.Services
{
    public class UserServiceClient(HttpClient httpClient, ILogger<UserServiceClient> logger) : IUserServiceClient
    {
        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"/api/users/{userId}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching user with ID {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> AssignUserToOrganizationAsync(int userId, int organizationId)
        {
            try
            {
                var content = JsonContent.Create(new { OrganizationId = organizationId });
                var response = await httpClient.PostAsync($"/api/users/{userId}/assign-organization", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error assigning user {UserId} to organization {OrganizationId}", userId, organizationId);
                throw;
            }
        }
    }
}