using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

public record ListSaleCommand : IRequest<PaginatedList<ListSaleResult>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}