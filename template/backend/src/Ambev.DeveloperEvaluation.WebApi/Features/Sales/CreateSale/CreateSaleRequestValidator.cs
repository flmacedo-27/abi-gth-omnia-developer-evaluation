using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    public CreateSaleRequestValidator(
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
            .SetValidator(new CreateSaleItemRequestValidator(_productRepository));

        RuleFor(sale => sale.TotalSaleAmount)
            .GreaterThan(0).WithMessage("Total sale value must be greater than 0.");
    }
}
