namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents a request to create a new product in the system.
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// Sets the name of the product. Must be between 3 and 100 characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Sets the code of the product. Must be unique.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Sets the description of the product (optional).
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Sets the price of the product. Must be greater than 0.
    /// </summary>
    public decimal Price { get; set; }
}