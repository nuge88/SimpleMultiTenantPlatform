using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using DocumentService.Application.Common.Interfaces;
using Shared.Contracts.UserContracts;

namespace DocumentService.Infrastructure.Services
{
    public class UserServiceClient(HttpClient httpClient, ILogger<UserServiceClient> logger) : IUserServiceClient
    {
        public async Task<UserDto?> GetUser(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"/api/users/{userId}");
                if (!response.IsSuccessStatusCode)
                {
                    logger.LogError("Failed to get user info for user ID {UserId}. Status code: {StatusCode}", userId, response.StatusCode);
                    return null;
                }

                var user = await response.Content.ReadFromJsonAsync<UserDto>();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching user info for user ID {UserId}", userId);
                throw;
            }
        }
    }
}