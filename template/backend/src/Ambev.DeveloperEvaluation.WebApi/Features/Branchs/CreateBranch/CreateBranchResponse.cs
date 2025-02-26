using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch;

/// <summary>
/// API response model for CreateBranch operation
/// </summary>
public class CreateBranchResponse
{
    /// <summary>
    /// The unique identifier of the created branch
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The branch's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The branch's code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// The branch's city
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// The branch's state
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// The branch's country
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// The branch's postal code
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    /// The branch's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The branch's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the branch
    /// </summary>
    public BranchStatus Status { get; set; }
}
