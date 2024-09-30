using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            bool useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                // Use In-Memory Database
                services.AddDbContext<UserDbContext>(options =>
                    options.UseInMemoryDatabase("UserDb"));
            }
            else
            {
                // Use SQL Server
                services.AddDbContext<UserDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }



            services.AddScoped<IUserRepository, UserRepository>();

            services.AddHttpClient<IOrganizationServiceClient, OrganizationServiceClient>(client =>
                {
                    var baseAddress = configuration["ServiceUrls:OrganizationService"];
                    if (string.IsNullOrEmpty(baseAddress))
                    {
                        throw new InvalidOperationException("OrganizationService base address is not configured.");
                    }
                    client.BaseAddress = new Uri(baseAddress);
                })
                //TODO THIS NEEDS TO BE REMOVED
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    // In development only, ignore SSL certificate validation errors
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                });


            return services;
        }
    }
}