using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CustomerIdValidator : AbstractValidator<Guid>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerIdValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public CustomerIdValidator()
    {
        RuleFor(customerId => customerId)
            .NotEmpty().WithMessage("The customerId is required.")
            .MustAsync(BeValidCustomerId).WithMessage("The CustomerId must be a valid ID that exists in the customer table.");
    }

    private async Task<bool> BeValidCustomerId(Guid customerId, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);

        return customer != null;
    }
}