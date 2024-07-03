namespace Todo.Api.Requests;

/// <summary>
/// Represents a request for creating a TODO item.
/// </summary>
/// <param name="Message">The item message.</param>
/// <param name="Title">The item title.</param>
public record CreateTodoRequest(string Message, string Title);
