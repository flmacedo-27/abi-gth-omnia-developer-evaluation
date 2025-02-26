using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a branch for sales identification.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Branch : BaseEntity, IBranch
{
    /// <summary>
    /// Gets the branch's name.
    /// Must not be null or empty and should contain name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch's code.
    /// Must not be null or empty and should contain unique branch code.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch's city.
    /// Must not be null or empty and should contain city where the branch is located.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch's state.
    /// Must not be null or empty and should contain state where the branch is located.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch's country.
    /// Must not be null or empty and should contain country where the branch is located.
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch's postal code.
    /// Must not be null or empty and should contain postal code where the branch is located.
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch's phone number.
    /// Must not be null or empty and should contain phone number the branch.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch's email.
    /// Must not be null or empty and should contain email the branch.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets the branch's current status.
    /// Indicates whether the branch is active, inactive in the system.
    /// </summary>
    public BranchStatus Status { get; set; }

    /// <summary>
    /// Gets the date and time when the branch was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the branch's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the unique identifier of the branch.
    /// </summary>
    /// <returns>The branch's ID as a string.</returns>
    string IBranch.Id => Id.ToString();

    /// <summary>
    /// Gets the branch code.
    /// </summary>
    /// <returns>The branch code.</returns>
    string IBranch.Code => Code;

    /// <summary>
    /// Initializes a new instance of the Branch class.
    /// </summary>
    public Branch()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the branch entity using the BranchValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new BranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Activates the branch.
    /// Changes the branch's status to Active.
    /// </summary>
    public void Activate()
    {
        Status = BranchStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the branch.
    /// Changes the branch's status to Inactive.
    /// </summary>
    public void Deactivate()
    {
        Status = BranchStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }
}
