using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch;

/// <summary>
/// Validator for CreateBranchRequest that defines validation rules for branch creation.
/// </summary>
public class CreateBranchRequestValidator : AbstractValidator<CreateBranchRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateBranchRequestValidator with defined validation rules.
    /// </summary>
    public CreateBranchRequestValidator()
    {
        RuleFor(branch => branch.Name)
            .NotEmpty().WithMessage("The name is required.")
            .Length(3, 100).WithMessage("The name must be between 3 and 100 characters.");

        RuleFor(branch => branch.Code)
            .NotEmpty().WithMessage("The code is required.")
            .Length(3, 10).WithMessage("The code must be between 3 and 10 characters.");

        RuleFor(branch => branch.City)
            .NotEmpty().WithMessage("The city is required.")
            .Length(3, 100).WithMessage("The city must be between 3 and 100 characters.");

        RuleFor(branch => branch.State)
            .NotEmpty().WithMessage("The state is required.")
            .Length(2, 100).WithMessage("The state must be between 2 and 100 characters.");

        RuleFor(branch => branch.Country)
            .NotEmpty().WithMessage("The country is required.")
            .Length(2, 100).WithMessage("The country must be between 2 and 100 characters.");

        RuleFor(branch => branch.PostalCode)
            .NotEmpty().WithMessage("The postal code is required.")
            .Matches(@"^\d{5}-?\d{3}$").WithMessage("The postal code must be in the format 12345-678 or 12345678.");

        RuleFor(branch => branch.Phone).SetValidator(new PhoneValidator());

        RuleFor(branch => branch.Email).SetValidator(new EmailValidator());
    }
}
