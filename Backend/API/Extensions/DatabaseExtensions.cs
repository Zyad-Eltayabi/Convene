using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace API.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        try
        {
            await context.Database.MigrateAsync();
            await DbInitializer.SeedData(context, userManager);
        }
        catch (Exception exception)
        {
            app.Logger.LogError(exception, "An error occurred while migrating or seeding the database.");
            throw;
        }
    }
}
