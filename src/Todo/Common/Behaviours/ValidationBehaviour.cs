using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Todo.Common.Behaviours;

/// <summary>
/// Represents a pipeline behaviour for validation.
/// </summary>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest: IRequest<TResponse>
{
    private readonly IReadOnlyList<IValidator<TRequest>> _validators;

    /// <summary>
    /// Constructs a new instance of <see cref="ValidationBehaviour{TRequest,TResponse}"/>.
    /// </summary>
    /// <param name="validators">The validators.</param>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators.ToList();
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Count == 0)
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var errors = new List<ValidationFailure>(_validators.Count);
        foreach (var validator in _validators)
        {
            var valid = await validator.ValidateAsync(context, cancellationToken).ConfigureAwait(false);
            if (valid?.Errors.Count > 0)
                errors.AddRange(valid.Errors);
        }

        return errors.Count == 0 ? await next() : throw new ValidationException(errors);
    }
}
