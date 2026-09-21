using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Activities.Validators;

public class CreateActivityValidator : AbstractValidator<CreateActivityDto>
{
    public CreateActivityValidator()
    {
        RuleFor(activity => activity.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 265).WithMessage("Title must be between 3 and 265 characters.");

        RuleFor(activity => activity.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(activity => activity.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 2000).WithMessage("Description must be between 10 and 2000 characters.");

        RuleFor(activity => activity.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Length(2, 100).WithMessage("Category must be between 2 and 100 characters.");

        RuleFor(activity => activity.City)
            .NotEmpty().WithMessage("City is required.")
            .Length(2, 100).WithMessage("City must be between 2 and 100 characters.");

        RuleFor(activity => activity.Venue)
            .NotEmpty().WithMessage("Venue is required.")
            .Length(2, 200).WithMessage("Venue must be between 2 and 200 characters.");
    }

}