using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

public record ListSaleCommand : IRequest<List<ListSaleResult>>
{
}