using Application.Activities.Commands;
using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Activities.Validators;

public class EditActivityValidator : BaseActivityValidator<EditActivity.Command, EditActivityDto>
{
    public EditActivityValidator() : base(x =>x.Activity)
    {
        RuleFor(x => x.Activity.Id)
            .NotEmpty().WithMessage("Id is required.");

    }
}