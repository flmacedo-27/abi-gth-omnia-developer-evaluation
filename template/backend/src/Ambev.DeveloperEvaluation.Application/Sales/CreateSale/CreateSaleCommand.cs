using Ambev.DeveloperEvaluation.Application.SalesItem.CreateSalesItem;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a sale, 
/// including number, date, customerId, branchId, items sale, and total sale amount. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateSaleResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSaleValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Sets the number of the sale to be created.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Sets the date of the sale to be created.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Sets the customerId of the sale to be created.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Sets the branchId of the sale to be created.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Sets the items of the sale to be created
    /// </summary>
    public List<CreateSaleItemCommand> Items { get; set; }

    /// <summary>
    /// Sets the total sale amount of the sale to be created.
    /// </summary>
    public decimal TotalSaleAmount { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}