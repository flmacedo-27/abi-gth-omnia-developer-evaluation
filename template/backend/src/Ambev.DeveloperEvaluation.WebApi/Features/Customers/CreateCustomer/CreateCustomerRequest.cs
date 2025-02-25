using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;

/// <summary>
/// Represents a request to create a new customer in the system.
/// </summary>
public class CreateCustomerRequest
{
    /// <summary>
    /// Sets the cpf or cnpj. Must be unique and contain only valid characters.
    /// </summary>
    public string CpfCnpj { get; set; } = string.Empty;

    /// <summary>
    /// Sets the full name. Must be between 3 and 100 characters.
    /// </summary>
    public string Fullname { get; set; } = string.Empty;

    /// <summary>
    /// Sets the phone number in format (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Sets the email address. Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Sets the initial status of the customer account.
    /// </summary>
    public CustomerStatus Status { get; set; }
}
