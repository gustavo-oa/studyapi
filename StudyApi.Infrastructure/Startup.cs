using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Infrastructure.Data;
using StudyApi.Infrastructure.Repositories;


namespace StudyApi.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbOptions>(configuration.GetSection(DbOptions.SectionName));
        services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddHostedService<DatabaseInitializer>();
        services.AddScoped<IOrderNumberRepository, OrderNumberRepository>();
        return services;
    }
}

