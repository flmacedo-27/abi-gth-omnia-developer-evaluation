using Ambev.DeveloperEvaluation.Application.SalesItem.DataObject;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale's number
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// The sale's date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The sale's customer ID
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The sale's branch ID
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The sale's items
    /// </summary>
    public List<SaleItemResult> Items { get; set; } = new();

    /// <summary>
    /// The total sale amount
    /// </summary>
    public decimal TotalSaleAmount { get; set; }

    /// <summary>
    /// The current status of the sale
    /// </summary>
    public SaleStatus Status { get; set; }
}