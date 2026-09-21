using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationException(context, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Error = "An unexpected error occurred." });
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
    private static async Task HandleValidationException(HttpContext context, ValidationException ex)
    {
        var errors = new Dictionary<string, string[]>();
        if (ex.Errors is not null)
        {
            foreach (var error in ex.Errors)
            {
                if (errors.ContainsKey(error.PropertyName))
                {
                    var existingErrors = errors[error.PropertyName];
                    var updatedErrors = existingErrors.Concat(new[] { error.ErrorMessage }).ToArray();
                    errors[error.PropertyName] = updatedErrors;
                }
                else
                {
                    errors[error.PropertyName] = [error.ErrorMessage];
                }
            }
        }
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var validationProblemDetails = new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "One or more validation errors occurred.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = "See the errors property for details.",
        };
        await context.Response.WriteAsJsonAsync(validationProblemDetails);
    }
}
