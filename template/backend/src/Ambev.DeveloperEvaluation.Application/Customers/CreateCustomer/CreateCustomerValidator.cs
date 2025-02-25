using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Validator for CreateCustomerCommand that defines validation rules for customer creation command.
/// </summary>
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCustomerCommandValidator with defined validation rules.
    /// </summary>
    public CreateCustomerCommandValidator()
    {
        RuleFor(customer => customer.Fullname)
            .NotEmpty().WithMessage("The Full name is required.")
            .Length(3, 100).WithMessage("The full name must be between 3 and 100 characters.");

        RuleFor(customer => customer.CpfCnpj).SetValidator(new CpfCnpjValidator());

        RuleFor(customer => customer.Email).SetValidator(new EmailValidator());

        RuleFor(customer => customer.Phone).SetValidator(new PhoneValidator());
    }
}