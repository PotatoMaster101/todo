using FluentResults;
using MediatR;
using Todo.Features.Todo.Domain;

namespace Todo.Features.Todo.DeleteTodo;

/// <summary>
/// Represents a request for deleting a TODO item.
/// </summary>
/// <param name="Id">The message ID.</param>
/// <param name="UserId">The ID of the user accessing the item.</param>
public record DeleteTodoCommand(Guid Id, string UserId) : IRequest<Result<TodoItem>>;
