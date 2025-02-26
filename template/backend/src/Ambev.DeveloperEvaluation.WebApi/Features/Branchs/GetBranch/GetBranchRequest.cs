namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.GetBranch;

/// <summary>
/// Request model for getting a branch by ID
/// </summary>
public class GetBranchRequest
{
    /// <summary>
    /// The unique identifier of the branch to retrieve
    /// </summary>
    public Guid Id { get; set; }
}

