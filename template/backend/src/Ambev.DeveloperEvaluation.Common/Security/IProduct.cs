namespace Ambev.DeveloperEvaluation.Common.Security;

public interface IProduct
{
    /// <summary>
    /// Gets the unique identifier of the product.
    /// </summary>
    /// <returns>The product's ID as a string.</returns>
    public string Id { get; }

    /// <summary>
    /// Gets the product code.
    /// </summary>
    /// <returns>The product code.</returns>
    public string Code { get; }
}
