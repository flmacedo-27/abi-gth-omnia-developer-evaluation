using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;

    public SaleValidator(
        ICustomerRepository customerRepository,
        IBranchRepository branchRepository,
        IProductRepository productRepository)
    {
        _customerRepository = customerRepository;
        _branchRepository = branchRepository;
        _productRepository = productRepository;

        RuleFor(sale => sale.Number)
            .NotNull().WithMessage("The sale number is required.")
            .GreaterThan(0).WithMessage("The sale number must be greater than 0.");

        RuleFor(sale => sale.Date).SetValidator(new DateValidator());

        RuleFor(sale => sale.CustomerId).SetValidator(new CustomerIdValidator(_customerRepository));

        RuleFor(sale => sale.BranchId).SetValidator(new BranchIdValidator(_branchRepository));

        RuleForEach(sale => sale.Items)
            .NotEmpty().WithMessage("The sale must contain at least one item.")
            .SetValidator(new SaleItemValidator(_productRepository));

        RuleFor(sale => sale.TotalSaleAmount)
            .GreaterThan(0).WithMessage("Total sale value must be greater than 0.");
    }
}

