using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.ListCustomer;

/// <summary>
/// Handler for processing ListCustomerCommand requests
/// </summary>
public class ListCustomerHandler : IRequestHandler<ListCustomerCommand, List<ListCustomerResult>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListCustomerHandler
    /// </summary>
    /// <param name="customerRepository">The customer repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for ListCustomerCommand</param>
    public ListCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListCustomerCommand request
    /// </summary>
    /// <param name="request">The ListCustomer command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The customer list</returns>
    public async Task<List<ListCustomerResult>> Handle(ListCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerList = await _customerRepository.GetAsync(cancellationToken);
        if (customerList.Count == 0)
            throw new KeyNotFoundException("The customer list is empty.");

        return _mapper.Map<List<ListCustomerResult>>(customerList);
    }
}
