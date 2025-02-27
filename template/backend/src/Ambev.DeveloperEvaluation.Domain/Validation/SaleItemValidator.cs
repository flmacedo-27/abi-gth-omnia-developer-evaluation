using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    private readonly IProductRepository _productRepository;

    public SaleItemValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(item => item.ProductId).SetValidator(new ProductIdValidator(_productRepository));

        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("The quantity value must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("It is not possible to sell more than 20 items for the same product.");

        RuleFor(item => item.UnitPrices)
            .GreaterThan(0).WithMessage("The unit prices value must be greater than 0.");

        RuleFor(item => item.Discount)
            .Must((item, discount) => ValidateDiscount(item.Quantity, discount))
            .WithMessage("Invalid discount for the given quantity.");

        RuleFor(item => item.TotalSaleItemAmount)
            .GreaterThan(0).WithMessage("The total sale item amount value must be greater than 0.");
    }

    private bool ValidateDiscount(int quantity, decimal? discount)
    {
        if (quantity < 4)
        {
            return discount == 0 || discount == null;
        }
        else if (quantity >= 4 && quantity < 10)
        {
            return discount == 0.10m;
        }
        else if (quantity >= 10 && quantity <= 20)
        {
            return discount == 0.20m;
        }

        return false;
    }
}
