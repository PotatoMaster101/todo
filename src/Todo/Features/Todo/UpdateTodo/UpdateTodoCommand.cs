using FluentResults;
using MediatR;
using Todo.Features.Todo.Domain;

namespace Todo.Features.Todo.UpdateTodo;

/// <summary>
/// Represents a request for updating a TODO item.
/// </summary>
/// <param name="Done">Whether the TODO is done.</param>
/// <param name="Id">The ID of the TODO.</param>
/// <param name="Message">The item message.</param>
/// <param name="Title">The item title.</param>
/// <param name="UserId">The ID of the user accessing the item.</param>
public record UpdateTodoCommand(
    bool Done,
    Guid Id,
    string Message,
    string Title,
    string UserId
) : IRequest<Result<TodoItem>>;
