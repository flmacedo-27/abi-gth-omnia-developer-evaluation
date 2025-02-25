using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customers.ListCustomer;

/// <summary>
/// Profile for mapping between Customer entity and ListCustomerResponse
/// </summary>
public class ListCustomerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCustomer operation
    /// </summary>
    public ListCustomerProfile()
    {
        CreateMap<Customer, ListCustomerResult>();
    }
}