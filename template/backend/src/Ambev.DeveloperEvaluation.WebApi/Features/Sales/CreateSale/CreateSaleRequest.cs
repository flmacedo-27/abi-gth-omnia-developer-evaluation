using Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale in the system.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Sets the number of the sale.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Sets the date of the sale process.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Sets the customerId of the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Sets the branchId of the sale.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Sets the items of the sale
    /// </summary>
    public List<CreateSaleItemRequest> Items { get; set; }

    /// <summary>
    /// Sets the total amount of the sale.
    /// </summary>
    public decimal TotalSaleAmount { get; set; }
}