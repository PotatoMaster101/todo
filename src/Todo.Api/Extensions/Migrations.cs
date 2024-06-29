using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Extensions;

/// <summary>
/// Extension methods for migrations.
/// </summary>
public static class Migrations
{
    /// <summary>
    /// Applies the DB migration.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <typeparam name="T">The type of the DB context.</typeparam>
    public static async Task ApplyMigrations<T>(this IApplicationBuilder builder)
        where T: DbContext
    {
        using var scope = builder.ApplicationServices.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<T>();
        await context.Database.MigrateAsync();
    }
}
