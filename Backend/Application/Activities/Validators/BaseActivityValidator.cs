using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Activities.Validators;

public class BaseActivityValidator<T,TDto> : AbstractValidator<T> where TDto : BaseActivityDto
{
    public BaseActivityValidator(Func<T,TDto> selector)
    {
        RuleFor(x => selector(x).Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 265).WithMessage("Title must be between 3 and 265 characters.");

        RuleFor(x => selector(x).Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(x => selector(x).Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 2000).WithMessage("Description must be between 10 and 2000 characters.");

        RuleFor(x => selector(x).Category)
            .NotEmpty().WithMessage("Category is required.")
            .Length(2, 100).WithMessage("Category must be between 2 and 100 characters.");

        RuleFor(x => selector(x).City)
            .NotEmpty().WithMessage("City is required.")
            .Length(2, 100).WithMessage("City must be between 2 and 100 characters.");

        RuleFor(x => selector(x).Venue)
            .NotEmpty().WithMessage("Venue is required.")
            .Length(2, 200).WithMessage("Venue must be between 2 and 200 characters.");

        RuleFor(x => selector(x).Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => selector(x).Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
    }

}