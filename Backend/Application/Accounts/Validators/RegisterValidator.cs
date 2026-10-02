using Application.Accounts.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Accounts.Validators;

public class RegisterValidator : AbstractValidator<Register.Command>
{
    public RegisterValidator(IOptions<IdentityOptions> identityOptions)
    {
        PasswordOptions passwordOptions = identityOptions.Value.Password;

        RuleFor(x => x.RegisterDto.DisplayName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(x => x.RegisterDto.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.RegisterDto.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(passwordOptions.RequiredLength)
            .Must(password => !passwordOptions.RequireNonAlphanumeric || password.Any(character => !char.IsLetterOrDigit(character)))
            .WithMessage("Password must contain a non-alphanumeric character.")
            .Must(password => !passwordOptions.RequireDigit || password.Any(char.IsDigit))
            .WithMessage("Password must contain a digit.")
            .Must(password => !passwordOptions.RequireLowercase || password.Any(char.IsLower))
            .WithMessage("Password must contain a lowercase letter.")
            .Must(password => !passwordOptions.RequireUppercase || password.Any(char.IsUpper))
            .WithMessage("Password must contain an uppercase letter.");
    }
}
