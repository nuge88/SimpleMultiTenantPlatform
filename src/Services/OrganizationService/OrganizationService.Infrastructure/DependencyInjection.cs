using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrganizationService.Application.Common.Interfaces;
using OrganizationService.Domain.Interfaces;
using OrganizationService.Infrastructure.Persistence;
using OrganizationService.Infrastructure.Repositories;
using OrganizationService.Infrastructure.Services;

namespace OrganizationService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            bool useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                // Use In-Memory Database
                services.AddDbContext<OrganizationDbContext>(options =>
                    options.UseInMemoryDatabase("OrganizationDb"));
            }
            else
            {
                // Use SQL Server
                services.AddDbContext<OrganizationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ServiceUrls:UserService"] ?? string.Empty);
            })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    //TODO: THIS NEEDS TO BE FIXED. 
                    // Check if we are in the Development environment
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
                    {
                        return new HttpClientHandler
                        {
                            // Ignore SSL certificate validation errors in Development environment
                            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                        };
                    }

                    return new HttpClientHandler(); // Default handler for other environments
                });

            // Register repositories
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();

            return services;
        }
    }
}