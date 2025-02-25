using Ambev.DeveloperEvaluation.Application.Customers.ListCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.ListCustomer;

/// <summary>
/// Profile for mapping ListCustomer feature requests to commands
/// </summary>
public class ListCustomerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListCustomer feature
    /// </summary>
    public ListCustomerProfile()
    {
        CreateMap<Guid, ListCustomerCommand>()
            .ConstructUsing(src => new ListCustomerCommand());
    }
}