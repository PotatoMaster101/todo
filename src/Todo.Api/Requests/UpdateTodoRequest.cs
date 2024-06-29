namespace Todo.Api.Requests;

/// <summary>
/// Represents a request for updating a TODO item.
/// </summary>
/// <param name="Done">Whether the TODO is done.</param>
/// <param name="Id">The ID of the TODO.</param>
/// <param name="Message">The item message.</param>
/// <param name="Title">The item title.</param>
public record UpdateTodoRequest(
    bool Done,
    Guid Id,
    string Message,
    string Title
);
