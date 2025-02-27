using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Handler for processing ListProductCommand requests
/// </summary>
public class ListProductHandler : IRequestHandler<ListProductCommand, PaginatedList<ListProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ListProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListProductCommand request
    /// </summary>
    /// <param name="request">The ListProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product list</returns>
    public async Task<PaginatedList<ListProductResult>> Handle(ListProductCommand request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetAsync(cancellationToken);
        if (productList.Count == 0)
            throw new KeyNotFoundException("The product list is empty.");

        var productListResult = _mapper.Map<List<ListProductResult>>(productList);

        return PaginatedList<ListProductResult>.Create(
            productListResult,
            request.PageNumber,
            request.PageSize
        );
    }
}