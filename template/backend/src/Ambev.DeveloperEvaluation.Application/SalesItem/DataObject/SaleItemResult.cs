namespace Ambev.DeveloperEvaluation.Application.SalesItem.DataObject;

public class SaleItemResult
{
    /// <summary>
    /// The product ID
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to the item
    /// </summary>
    public decimal? Discount { get; set; }

    /// <summary>
    /// The total amount for the item
    /// </summary>
    public decimal TotalItemAmount { get; set; }
}
