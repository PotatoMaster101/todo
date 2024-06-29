using FluentValidation;

namespace Todo.Features.Todo.CreateTodo;

/// <summary>
/// Represents a validator for <see cref="CreateTodoCommand"/>.
/// </summary>
// ReSharper disable once UnusedType.Global
public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    /// <summary>
    /// Constructs a new instance for <see cref="CreateTodoCommandValidator"/>.
    /// </summary>
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Message).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(Constants.MaxTitleLength);
    }
}
