using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Features.Todo.Domain;
using Todo.Features.Todo.Infrastructure;

namespace Todo.Features.Todo.DeleteTodo;

/// <summary>
/// Represents a handler for deleting a TODO item.
/// </summary>
// ReSharper disable once UnusedType.Global
public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, Result<TodoItem>>
{
    private readonly ITodoDbContext _dbContext;

    /// <summary>
    /// Constructs a new instance of <see cref="DeleteTodoHandler"/>.
    /// </summary>
    public DeleteTodoHandler(ITodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<Result<TodoItem>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Items.Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (item is null || item.CreationUserId != request.UserId)
            return Result.Fail<TodoItem>("item not found");

        _dbContext.Items.Remove(item);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return item;
    }
}
