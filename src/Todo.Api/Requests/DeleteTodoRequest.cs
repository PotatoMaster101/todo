namespace Todo.Api.Requests;

/// <summary>
/// Represents a request for deleting a TODO item.
/// </summary>
/// <param name="Id">The message ID.</param>
public record DeleteTodoRequest(Guid Id);
