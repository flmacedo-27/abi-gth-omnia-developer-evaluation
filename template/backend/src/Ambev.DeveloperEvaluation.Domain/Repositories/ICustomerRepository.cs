using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Customer entity operations
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Creates a new customer in the repository
    /// </summary>
    /// <param name="customer">The customer to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created customer</returns>
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a customers list
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer list, empty list otherwise</returns>
    Task<List<Customer>> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a cutomer by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer if found, null otherwise</returns>
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a cutomer by their cpf or cnpj
    /// </summary>
    /// <param name="cpfcnpj">The cpf or cnpj of the customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer if found, null otherwise</returns>
    Task<Customer?> GetByCpfCnpjAsync(string cpfcnpj, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a customer from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the customer to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the customer was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
