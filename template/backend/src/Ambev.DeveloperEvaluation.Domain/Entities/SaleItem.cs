using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents sale item.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity, ISaleItem
{
    private readonly IValidator<SaleItem> _validator;

    /// <summary>
    /// Gets the sales item productId.
    /// It must be a valid productId that exists in the product table.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets the sales item product.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Gets the sales item quantity.
    /// The quantity must be greater than 0.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the sales item unit prices.
    /// The unit prices must be greater than 0.
    /// </summary>
    public decimal UnitPrices { get; set; }

    /// <summary>
    /// Gets the sales item discounts.
    /// Unit prices must be greater than 0 when quantity is above 4.
    /// </summary>
    public decimal? Discount { get; set; }

    /// <summary>
    /// Gets the sales item total item amount.
    /// The Total amount for each item must be greater than 0.
    /// </summary>
    public decimal TotalSaleItemAmount { get; set; }

    /// <summary>
    /// Gets the date and time when the sale item was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the sale item's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the unique identifier of the sale item.
    /// </summary>
    /// <returns>The sale's item ID as a string.</returns>
    string ISaleItem.Id => Id.ToString();

    public SaleItem(IValidator<SaleItem> validator)
    {
        _validator = validator;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the sale item entity using the SaleItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var result = _validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}