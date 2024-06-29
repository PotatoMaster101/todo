using MediatR;
using Todo.Features.Todo.Domain;

namespace Todo.Features.Todo.CreateTodo;

/// <summary>
/// Represents a request for creating a TODO item.
/// </summary>
/// <param name="Message">The item message.</param>
/// <param name="Title">The item title.</param>
/// <param name="UserId">The ID of the user accessing the item.</param>
public record CreateTodoCommand(
    string Message,
    string Title,
    string UserId
) : IRequest<TodoItem>;
