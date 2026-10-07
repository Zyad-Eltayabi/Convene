using Application.Interfaces;
using Infrastructure.Persistence.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddApplicationDbContext(services, configuration);
        RegisterRepositories(services);
        return services;
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserAccessor, UserAccessor>();
    }

    private static void AddApplicationDbContext(IServiceCollection services, IConfiguration configuration)
    {
        // Add database context using SQL Server.
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}

