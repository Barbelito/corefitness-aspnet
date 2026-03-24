using Infrastructure.Persistence.EfCore.Contexts;
using Infrastructure.Persistence.EfCore.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence;

public static class PersistenceRegistrationExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddContexts(configuration, environment);
        services.AddRepositories(configuration, environment);
        return services;
    }
}
