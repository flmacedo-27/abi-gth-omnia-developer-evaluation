using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

/// <summary>
/// Profile for mapping between Sale entity and ListSaleResult
/// </summary>
public class ListSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSale operation
    /// </summary>
    public ListSaleProfile()
    {
        CreateMap<Sale, ListSaleResult>();
    }
}