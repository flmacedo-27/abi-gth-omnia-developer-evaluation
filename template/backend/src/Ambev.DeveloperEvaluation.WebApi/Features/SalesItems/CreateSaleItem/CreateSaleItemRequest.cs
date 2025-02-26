namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;

/// <summary>
/// Represents a request to create a new sale item in the system.
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Sets the productId of the sale item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Sets the quantity of the product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Sets the unit prices of the sale item.
    /// </summary>
    public decimal UnitPrices { get; set; }

    /// <summary>
    /// Sets the discount of the sale item.
    /// </summary>
    public decimal? Discount { get; set; }

    /// <summary>
    /// Sets the total amount of the sale item.
    /// </summary>
    public decimal TotalSaleItemAmount { get; set; }
}
