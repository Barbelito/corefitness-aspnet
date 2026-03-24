using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddPersistence(configuration, environment);
        return services;
    }
}
