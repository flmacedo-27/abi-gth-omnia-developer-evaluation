using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;

/// <summary>
/// Validator for CreateSaleItemRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    private readonly IProductRepository _productRepository;
 
    /// <summary>
    /// Initializes a new instance of the CreateSaleItemRequestValidator with defined validation rules.
    /// </summary>
    public CreateSaleItemRequestValidator(IProductRepository productRepository)
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
