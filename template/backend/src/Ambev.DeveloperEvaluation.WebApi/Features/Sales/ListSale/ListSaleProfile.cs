using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;

/// <summary>
/// Profile for mapping ListSale feature requests to commands
/// </summary>
public class ListSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSale feature
    /// </summary>
    public ListSaleProfile()
    {
        CreateMap<Guid, ListSaleCommand>()
            .ConstructUsing(src => new ListSaleCommand());
    }
}