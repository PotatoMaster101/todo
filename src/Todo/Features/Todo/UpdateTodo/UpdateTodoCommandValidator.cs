using FluentValidation;

namespace Todo.Features.Todo.UpdateTodo;

/// <summary>
/// Represents a validator for <see cref="UpdateTodoCommand"/>.
/// </summary>
// ReSharper disable once UnusedType.Global
public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    /// <summary>
    /// Constructs a new instance for <see cref="UpdateTodoCommandValidator"/>.
    /// </summary>
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Message).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(Constants.MaxTitleLength);
    }
}
