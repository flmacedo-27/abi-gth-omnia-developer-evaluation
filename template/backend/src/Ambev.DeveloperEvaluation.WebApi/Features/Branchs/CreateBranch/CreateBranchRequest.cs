using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch;

/// <summary>
/// Represents a request to create a new branch in the system.
/// </summary>
public class CreateBranchRequest
{
    /// <summary>
    /// Sets the name. Must be between 3 and 100 characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Sets the code. Must be between 3 and 10 characters.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Sets the city. Must be between 3 and 100 characters.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Sets the state. Must be between 2 and 100 characters.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Sets the country. Must be between 2 and 100 characters.
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Sets the postal code in format 12345-678 or 12345678.
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;


    /// <summary>
    /// Sets the phone number in format (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Sets the email address. Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Sets the initial status of the branch account.
    /// </summary>
    public BranchStatus Status { get; set; }
}
