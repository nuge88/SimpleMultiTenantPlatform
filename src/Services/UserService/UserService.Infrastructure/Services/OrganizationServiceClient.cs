using UserService.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace UserService.Infrastructure.Services
{
    public class OrganizationServiceClient(HttpClient httpClient, ILogger<OrganizationServiceClient> logger) : IOrganizationServiceClient
    {

        public async Task<bool> OrganizationExistsAsync(int organizationId)
        {
            try
            {
                var response = await httpClient.GetAsync($"/api/organizations/{organizationId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching organization with ID {OrganizationId}", organizationId);
                throw;
            }
        }
    }
}