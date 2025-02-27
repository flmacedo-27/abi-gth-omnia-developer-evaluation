using Ambev.DeveloperEvaluation.Application.SalesItem.CreateSalesItem;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    public CreateSaleValidator(
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
            .SetValidator(new CreateSaleItemValidator(_productRepository));

        RuleFor(sale => sale.TotalSaleAmount)
            .GreaterThan(0).WithMessage("Total sale value must be greater than 0.");
    }
}
