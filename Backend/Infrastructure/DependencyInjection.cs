using Application.Interfaces;
using Infrastructure.Photos;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddApplicationDbContext(services, configuration);
        RegisterServices(services);
        return services;
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddSingleton<CloudinarySettings>();
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

