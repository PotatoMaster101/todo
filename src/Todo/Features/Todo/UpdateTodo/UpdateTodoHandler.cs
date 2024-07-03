using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Features.Todo.Domain;
using Todo.Features.Todo.Infrastructure;

namespace Todo.Features.Todo.UpdateTodo;

/// <summary>
/// Represents a handler for updating a TODO item.
/// </summary>
// ReSharper disable once UnusedType.Global
public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, Result<TodoItem>>
{
    private readonly ITodoDbContext _dbContext;

    /// <summary>
    /// Constructs a new instance of <see cref="UpdateTodoHandler"/>.
    /// </summary>
    public UpdateTodoHandler(ITodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<Result<TodoItem>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Items.Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (item is null || item.CreationUserId != request.UserId)
            return Result.Fail<TodoItem>("item not found");

        item.UpdateDate = DateTime.Now.ToUniversalTime();
        item.Message = request.Message;
        item.Title = request.Title;
        item.Done = request.Done;
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return item;
    }
}
