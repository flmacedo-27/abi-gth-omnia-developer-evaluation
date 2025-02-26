namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Response model for ListProduct operation
/// </summary>
public class ListProductResult
{
    /// <summary>
    /// The ID of the product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the product
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The code of the product
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// The description of the product (optional)
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The price of the product
    /// </summary>
    public decimal Price { get; set; }
}
