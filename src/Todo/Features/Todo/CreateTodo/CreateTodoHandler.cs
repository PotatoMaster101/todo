using MediatR;
using Todo.Features.Todo.Domain;
using Todo.Features.Todo.Infrastructure;

namespace Todo.Features.Todo.CreateTodo;

/// <summary>
/// Represents a handler for creating a TODO item.
/// </summary>
// ReSharper disable once UnusedType.Global
public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, TodoItem>
{
    private readonly ITodoDbContext _dbContext;

    /// <summary>
    /// Constructs a new instance of <see cref="CreateTodoHandler"/>.
    /// </summary>
    public CreateTodoHandler(ITodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<TodoItem> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var item = request.ToDomain();
        await _dbContext.Items.AddAsync(item, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return item;
    }
}
