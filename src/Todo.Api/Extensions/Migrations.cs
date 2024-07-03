using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Constants;
using Todo.Common.Authentication;

namespace Todo.Api.Extensions;

/// <summary>
/// Extension methods for migrations.
/// </summary>
public static class Migrations
{
    /// <summary>
    /// Adds an admin claim.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    public static async Task AddAdminClaim(this IApplicationBuilder builder)
    {
        await using var scope = builder.ApplicationServices.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();

        // assume first claim is admin
        var adminClaim = await context.UserClaims.Where(x => x.Id == 1)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
        if (adminClaim is not null)
            return;

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        await context.AddAsync(new IdentityUserClaim<string>
        {
            ClaimType = ApplicationClaimTypes.Admin,
            ClaimValue = "true",
            Id = 1,
            UserId = config["AdminUser:Id"]!,
        }).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Adds an admin user to the authentication DB.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    public static async Task AddAdminUser(this IApplicationBuilder builder)
    {
        await using var scope = builder.ApplicationServices.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var adminSection = config.GetRequiredSection("AdminUser");
        var adminUser = await context.Users.Where(x => x.Id == adminSection["Id"])
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
        if (adminUser is not null)
            return;

        var user = new IdentityUser
        {
            Email = adminSection["Email"],
            EmailConfirmed = true,
            Id = adminSection["Id"]!,
            NormalizedEmail = adminSection["NormalizedEmail"],
            NormalizedUserName = adminSection["NormalizedUserName"],
            UserName = adminSection["UserName"],
        };

        var hasher = new PasswordHasher<IdentityUser>();
        user.PasswordHash = hasher.HashPassword(user, adminSection["Password"]!);
        await context.AddAsync(user).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Applies the DB migration.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <typeparam name="T">The type of the DB context.</typeparam>
    public static async Task ApplyMigrations<T>(this IApplicationBuilder builder)
        where T: DbContext
    {
        await using var scope = builder.ApplicationServices.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<T>();
        await context.Database.MigrateAsync().ConfigureAwait(false);
    }
}
