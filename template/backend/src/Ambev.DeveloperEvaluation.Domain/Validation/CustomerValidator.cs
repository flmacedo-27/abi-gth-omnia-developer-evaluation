using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator() 
    {
        RuleFor(customer => customer.Fullname)
            .NotEmpty().WithMessage("The Full name is required.")
            .Length(3, 100).WithMessage("The full name must be between 3 and 100 characters.");

        RuleFor(customer => customer.CpfCnpj).SetValidator(new CpfCnpjValidator());

        RuleFor(customer => customer.Email).SetValidator(new EmailValidator());

        RuleFor(customer => customer.Phone).SetValidator(new PhoneValidator());
    }
}
