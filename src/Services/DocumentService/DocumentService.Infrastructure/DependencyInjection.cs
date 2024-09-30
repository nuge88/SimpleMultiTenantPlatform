using DocumentService.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DocumentService.Domain.Interfaces;
using DocumentService.Infrastructure.Persistence;
using DocumentService.Infrastructure.Repositories;
using DocumentService.Infrastructure.Services;

namespace DocumentService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            bool useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                // Use In-Memory Database
                services.AddDbContext<DocumentDbContext>(options =>
                    options.UseInMemoryDatabase("DocumentDb"));
            }
            else
            {
                // Use SQL Server
                services.AddDbContext<DocumentDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }


            // Register repositories
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();

            services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
            {
                var baseAddress = configuration["ServiceUrls:UserService"];
                if (string.IsNullOrEmpty(baseAddress))
                {
                    throw new InvalidOperationException("UserService base address is not configured.");
                }
                client.BaseAddress = new Uri(baseAddress);
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

            return services;
        }
    }
}