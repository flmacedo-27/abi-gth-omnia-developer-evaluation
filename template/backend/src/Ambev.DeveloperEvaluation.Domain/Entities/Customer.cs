using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a customer for sales identification.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Customer : BaseEntity, ICustomer
{
    /// <summary>
    /// Gets the customer's full name.
    /// Must not be null or empty and should contain both first and last names.
    /// </summary>
    public string Fullname { get; set; } = string.Empty;

    /// <summary>
    /// Gets the customer's cpf or cnpj.
    /// Must be a valid cpf or cnpj format.
    /// </summary>
    public string CpfCnpj { get; set; } = string.Empty;

    /// <summary>
    /// Gets the customer's email address.
    /// Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets the customer's phone number.
    /// Must be a valid phone number format following the pattern (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets the customer's current status.
    /// Indicates whether the customer is active, inactive in the system.
    /// </summary>
    public CustomerStatus Status { get; set; }

    /// <summary>
    /// Gets the date and time when the customer was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the customer's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the unique identifier of the customer.
    /// </summary>
    /// <returns>The customer's ID as a string.</returns>
    string ICustomer.Id => Id.ToString();

    /// <summary>
    /// Gets the cpf or cnpj.
    /// </summary>
    /// <returns>The cpf or cnpj.</returns>
    string ICustomer.CpfCnpj => CpfCnpj;

    /// <summary>
    /// Initializes a new instance of the Customer class.
    /// </summary>
    public Customer()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the customer entity using the CustomerValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CustomerValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Activates the customer.
    /// Changes the customer's status to Active.
    /// </summary>
    public void Activate()
    {
        Status = CustomerStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the customer.
    /// Changes the customer's status to Inactive.
    /// </summary>
    public void Deactivate()
    {
        Status = CustomerStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }
}
