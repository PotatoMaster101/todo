using Microsoft.EntityFrameworkCore;
using Todo.Features.Todo.Domain;

namespace Todo.Features.Todo.Infrastructure;

/// <summary>
/// Represents an implementation of <see cref="ITodoDbContext"/>
/// </summary>
public class TodoDbContext : DbContext, ITodoDbContext
{
    /// <inheritdoc />
    public DbSet<TodoItem> Items { get; set; }

    /// <inheritdoc />
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options) { }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("todo");
    }
}
