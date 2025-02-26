using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Branch entity operations
/// </summary>
public interface IBranchRepository
{
    /// <summary>
    /// Creates a new branch in the repository
    /// </summary>
    /// <param name="branch">The branch to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created branch</returns>
    Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a branchs list
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch list, empty list otherwise</returns>
    Task<List<Branch>> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a branch by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a branch by their code
    /// </summary>
    /// <param name="code">The code of the branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    Task<Branch?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a branch from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the branch was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
