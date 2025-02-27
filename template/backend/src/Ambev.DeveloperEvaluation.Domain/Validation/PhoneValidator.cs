using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        RuleFor(phone => phone)
            .NotEmpty().WithMessage("The phone cannot be empty.")
            .Matches(@"^(\(\d{2}\) \d{4,5}-\d{4}|[1-9]\d{9,10}|\+\d{11,13})$")
            .WithMessage("The phone format is not valid. Expected formats: (XX) XXXXX-XXXX, 11999999999, or +551199999999.");
    }
}
