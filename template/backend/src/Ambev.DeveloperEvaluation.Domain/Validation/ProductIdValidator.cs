using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductIdValidator : AbstractValidator<Guid>
{
    private readonly IProductRepository _productRepository;

    public ProductIdValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(productId => productId)
            .NotEmpty().WithMessage("O productId is required.")
            .MustAsync(BeValidProductId).WithMessage("The ProductId must be a valid ID that exists in the product table.");
    }

    private async Task<bool> BeValidProductId(Guid productId, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        return product != null;
    }
}