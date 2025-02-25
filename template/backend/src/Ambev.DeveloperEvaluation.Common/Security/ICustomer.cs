namespace Ambev.DeveloperEvaluation.Common.Security;

public interface ICustomer
{
    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    /// <returns>The user's ID as a string.</returns>
    public string Id { get; }

    /// <summary>
    /// Gets the customer name.
    /// </summary>
    /// <returns>The customer name.</returns>
    public string Fullname { get; }

    /// <summary>
    /// Gets the customer's CPF or CNPJ.
    /// </summary>
    /// <returns>The customer's CPF or CNPJ.</returns>
    public string CpfCnpj { get; }
}
