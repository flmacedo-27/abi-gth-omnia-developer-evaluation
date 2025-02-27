using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

/// <summary>
/// Handler for processing ListSaleCommand requests
/// </summary>
public class ListSaleHandler : IRequestHandler<ListSaleCommand, PaginatedList<ListSaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ListSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListSaleCommand request
    /// </summary>
    /// <param name="request">The ListSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale list</returns>
    public async Task<PaginatedList<ListSaleResult>> Handle(ListSaleCommand request, CancellationToken cancellationToken)
    {
        var saleList = await _saleRepository.GetAsync(cancellationToken);
        if (saleList.Count == 0)
            throw new KeyNotFoundException("The sale list is empty.");

        var saleListResult = _mapper.Map<List<ListSaleResult>>(saleList);
        
        return PaginatedList<ListSaleResult>.Create(
            saleListResult,
            request.PageNumber,
            request.PageSize
        );
    }
}