using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents the application's sales.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity, ISale
{
    /// <summary>
    /// Gets the sale's number.
    /// Must not be null.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets the sale's date.
    /// Must be a valid date.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets the sale's customerId.
    /// It must be a valid customerId that exists in the customer table.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets the sale's customer.
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// Gets the sale's branchId.
    /// It must be a valid branchId that exists in the branch table.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets the sale's branch.
    /// </summary>
    public Branch Branch { get; set; }

    /// <summary>
    /// Gets the sale's items.
    /// </summary>
    public List<SaleItem> Items { get; set; }

    /// <summary>
    /// Total sale amount
    /// The sale amount must be greater than 0.
    /// </summary>
    public decimal TotalSaleAmount { get; set; }

    /// <summary>
    /// Gets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the sale's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the unique identifier of the sale.
    /// </summary>
    /// <returns>The sale's ID as a string.</returns>
    string ISale.Id => Id.ToString();

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
