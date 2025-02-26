using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.Number)
            .NotNull().WithMessage("The sale number is required.")
            .GreaterThan(0).WithMessage("The sale number must be greater than 0.");

        RuleFor(sale => sale.Date).SetValidator(new DateValidator());

        RuleFor(sale => sale.CustomerId).SetValidator(new CustomerIdValidator());

        RuleFor(sale => sale.BranchId).SetValidator(new BranchIdValidator());

        RuleForEach(sale => sale.Items)
            .NotEmpty().WithMessage("The sale must contain at least one item.")
            .SetValidator(new SaleItemValidator());

        RuleFor(sale => sale.TotalSaleAmount)
            .GreaterThan(0).WithMessage("Total sale value must be greater than 0.");
    }
}

