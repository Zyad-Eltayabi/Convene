using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Core;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator == null)
        {
            return await next();
        }
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        return !validationResult.IsValid ? throw new ValidationException(validationResult.Errors) : await next();
    }
}
