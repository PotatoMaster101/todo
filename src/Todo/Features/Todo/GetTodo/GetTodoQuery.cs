using FluentResults;
using MediatR;
using Todo.Features.Todo.Domain;

namespace Todo.Features.Todo.GetTodo;

/// <summary>
/// Represents a request for getting a TODO item.
/// </summary>
/// <param name="Id">The ID of the TODO item.</param>
/// <param name="UserId">The ID of the user accessing the item.</param>
public record GetTodoByIdQuery(Guid Id, string UserId) : IRequest<Result<TodoItem>>;

/// <summary>
/// Represents a request for getting a TODO item.
/// </summary>
/// <param name="UserId">The ID of the user accessing the item.</param>
public record GetTodoByUserQuery(string UserId) : IRequest<IQueryable<TodoItem>>;
