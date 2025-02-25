using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;

/// <summary>
/// Validator for CreateCustomerRequest that defines validation rules for customer creation.
/// </summary>
public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCustomerRequestValidator with defined validation rules.
    /// </summary>
    public CreateCustomerRequestValidator()
    {
        RuleFor(customer => customer.Fullname)
            .NotEmpty().WithMessage("The Full name is required.")
            .Length(3, 100).WithMessage("The full name must be between 3 and 100 characters.");

        RuleFor(customer => customer.CpfCnpj).SetValidator(new CpfCnpjValidator());

        RuleFor(customer => customer.Email).SetValidator(new EmailValidator());

        RuleFor(customer => customer.Phone).SetValidator(new PhoneValidator());
    }
}
