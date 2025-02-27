using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

public record ListProductCommand : IRequest<PaginatedList<ListProductResult>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}