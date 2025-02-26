using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class DateValidator : AbstractValidator<DateTime>
{
    public DateValidator()
    {
        RuleFor(date => date)
            .NotEmpty().WithMessage("The sale date is required.")
            .Must(BeValidDate).WithMessage("The sale date must be a valid date.");
    }

    private bool BeValidDate(DateTime date)
    {
        return date != default && date <= DateTime.Now;
    }
}
