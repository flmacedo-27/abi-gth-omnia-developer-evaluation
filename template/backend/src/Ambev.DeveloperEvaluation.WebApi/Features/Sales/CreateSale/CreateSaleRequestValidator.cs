using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.Number)
            .NotNull().WithMessage("The sale number is required.")
            .GreaterThan(0).WithMessage("The sale number must be greater than 0.");

        RuleFor(sale => sale.Date).SetValidator(new DateValidator());

        RuleFor(sale => sale.CustomerId).SetValidator(new CustomerIdValidator());

        RuleFor(sale => sale.BranchId).SetValidator(new BranchIdValidator());

        RuleForEach(sale => sale.Items)
            .NotEmpty().WithMessage("The sale must contain at least one item.")
            .SetValidator(new CreateSaleItemRequestValidator());

        RuleFor(sale => sale.TotalSaleAmount)
            .GreaterThan(0).WithMessage("Total sale value must be greater than 0.");
    }
}
