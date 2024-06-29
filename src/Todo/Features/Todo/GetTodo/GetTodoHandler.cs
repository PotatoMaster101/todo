using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Features.Todo.Domain;
using Todo.Features.Todo.Infrastructure;

namespace Todo.Features.Todo.GetTodo;

/// <summary>
/// Represents a handler for getting a TODO item.
/// </summary>
// ReSharper disable once UnusedType.Global
public class GetTodoHandler :
    IRequestHandler<GetTodoByIdQuery, Result<TodoItem>>,
    IRequestHandler<GetTodoByUserQuery, IQueryable<TodoItem>>
{
    private readonly ITodoDbContext _dbContext;

    /// <summary>
    /// Constructs a new instance of <see cref="GetTodoHandler"/>.
    /// </summary>
    public GetTodoHandler(ITodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<Result<TodoItem>> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Items
            .Where(item => item.Id == request.Id && item.CreationUserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        return item ?? Result.Fail<TodoItem>("item not found");
    }

    /// <inheritdoc />
    public Task<IQueryable<TodoItem>> Handle(GetTodoByUserQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dbContext.Items.Where(x => x.CreationUserId == request.UserId));
    }
}
