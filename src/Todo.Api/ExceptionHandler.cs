using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api;

/// <summary>
/// Represents an exception handler.
/// </summary>
public class ExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    /// <summary>
    /// Constructs a new instance of <see cref="ExceptionHandler"/>.
    /// </summary>
    public ExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    /// <inheritdoc />
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception is ValidationException
            ? StatusCodes.Status422UnprocessableEntity
            : StatusCodes.Status400BadRequest;

        return _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = GetProblemDetailsFromException(exception),
        });
    }

    /// <summary>
    /// Returns a <see cref="ProblemDetails"/> based on the exception.
    /// </summary>
    /// <param name="exception">The exception to create the <see cref="ProblemDetails"/>.</param>
    /// <returns>The created <see cref="ProblemDetails"/>.</returns>
    private static ProblemDetails GetProblemDetailsFromException(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage });

            return new ProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Extensions = new Dictionary<string, object?> { ["errors"] = errors },
            };
        }

        return new ProblemDetails
        {
            Detail = exception.Message,
            Title = "An error occurred while processing your request.",
        };
    }
}
