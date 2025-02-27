using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.ListCustomer;

public record ListCustomerCommand : IRequest<PaginatedList<ListCustomerResult>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
