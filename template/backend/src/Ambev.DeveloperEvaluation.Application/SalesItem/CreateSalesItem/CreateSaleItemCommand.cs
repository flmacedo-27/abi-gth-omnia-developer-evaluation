namespace Ambev.DeveloperEvaluation.Application.SalesItem.CreateSalesItem;

public class CreateSaleItemCommand
{
    /// <summary>
    /// Sets the productId of the sale item to be created.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Sets the quantity of the sale item to be created.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Sets the unit prices of the sale item to be created.
    /// </summary>
    public decimal UnitPrices { get; set; }

    /// <summary>
    /// Sets the discount of the sale item to be created.
    /// </summary>
    public decimal? Discount { get; set; }

    /// <summary>
    /// Sets the total amount of the sale item to be created.
    /// </summary>
    public decimal TotalSaleItemAmount { get; set; }
}
