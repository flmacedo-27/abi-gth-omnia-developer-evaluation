using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.ListCustomer;

public record ListCustomerCommand : IRequest<List<ListCustomerResult>>
{
}
