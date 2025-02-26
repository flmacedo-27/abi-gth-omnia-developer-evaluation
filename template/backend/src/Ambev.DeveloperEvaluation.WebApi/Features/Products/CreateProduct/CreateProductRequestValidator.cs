using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for product creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
    /// </summary>
    public CreateProductRequestValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("The name is required.")
            .Length(3, 100).WithMessage("The name must be between 3 and 100 characters.");

        RuleFor(product => product.Code)
            .NotEmpty().WithMessage("The code is required.")
            .Length(3, 10).WithMessage("The code must be between 3 and 10 characters.");

        RuleFor(product => product.Description)
            .MaximumLength(500).WithMessage("The description must be up to 500 characters.")
            .When(product => !string.IsNullOrEmpty(product.Description));

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("The price must be greater than 0.");
    }
}