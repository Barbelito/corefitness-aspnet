using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.EfCore.Contexts;

public static class ContextRegistrationExtension
{
    public static IServiceCollection AddContexts(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        if(environment.IsDevelopment())
        {
            services.AddSingleton<SqliteConnection>(_ =>
            {
                var connection = new SqliteConnection("Data Source=:memory:;");
                connection.Open();
                return connection;
            });

            services.AddDbContext<DataContext>((sp, options) =>
            {
                var connection = sp.GetRequiredService<SqliteConnection>();
                options.UseSqlite(connection);
            });
        }
        else
        {
            services.AddDbContext<DataContext>((sp, options) =>
            {
                var connection = configuration.GetConnectionString("ProductionDatabaseUri") ?? throw new ArgumentException("Production Database Connection must be provided.");
                options.UseSqlServer(connection);
            });
        }

        return services;
    }
}
