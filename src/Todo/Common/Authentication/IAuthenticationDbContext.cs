using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Todo.Common.Authentication;

/// <summary>
/// Represents a DB context for authentication.
/// </summary>
public interface IAuthenticationDbContext
{
    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    DbSet<IdentityRole> Roles { get; set; }

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    DbSet<IdentityUserRole<string>> UserRoles { get; set; }

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    DbSet<IdentityUser> Users { get; set; }
}
