using Todo.Features.Todo.CreateTodo;
using Todo.Features.Todo.Domain;

namespace Todo.Features.Todo;

/// <summary>
/// Mapper for domain objects and requests.
/// </summary>
public static class Mapper
{
    /// <summary>
    /// Maps <see cref="CreateTodoCommand"/> to <see cref="TodoItem"/>.
    /// </summary>
    /// <param name="request">The request to map.</param>
    /// <returns>The mapped object.</returns>
    public static TodoItem ToDomain(this CreateTodoCommand request)
    {
        return new TodoItem
        {
            CreationUserId = request.UserId,
            Message = request.Message,
            Title = request.Title,
        };
    }
}
