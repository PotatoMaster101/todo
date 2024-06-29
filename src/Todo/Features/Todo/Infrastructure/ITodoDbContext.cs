using Microsoft.EntityFrameworkCore;
using Todo.Features.Todo.Domain;

namespace Todo.Features.Todo.Infrastructure;

/// <summary>
/// Represents a DB context for <see cref="TodoItem"/>.
/// </summary>
public interface ITodoDbContext
{
    /// <summary>
    /// Gets or sets the todo items.
    /// </summary>
    DbSet<TodoItem> Items { get; set; }

    /// <inheritdoc cref="DbContext.SaveChangesAsync(System.Threading.CancellationToken)"/>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
