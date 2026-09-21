using Application.Core;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger,IHostEnvironment host) : IMiddleware
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
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private  async Task HandleException(HttpContext context, Exception ex)
    {
        logger.LogError($"Message : {ex.Message} :::: Exception: {ex}");
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var response = host.IsDevelopment()
            ? new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace)
            : new AppException(context.Response.StatusCode, "An unexpected error occurred.",null);
        var options = new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase };
        var json = System.Text.Json.JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsJsonAsync(response);
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
