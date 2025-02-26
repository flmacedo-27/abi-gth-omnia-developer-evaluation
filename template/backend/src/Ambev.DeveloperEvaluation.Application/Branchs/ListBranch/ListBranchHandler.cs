using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.ListBranch;

/// <summary>
/// Handler for processing ListBranchCommand requests
/// </summary>
public class ListBranchHandler : IRequestHandler<ListBranchCommand, List<ListBranchResult>>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ListBranchHandler
    /// </summary>
    /// <param name="branchRepository">The branch repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for ListBranchCommand</param>
    public ListBranchHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListBranchCommand request
    /// </summary>
    /// <param name="request">The ListBranch command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch list</returns>
    public async Task<List<ListBranchResult>> Handle(ListBranchCommand request, CancellationToken cancellationToken)
    {
        var branchList = await _branchRepository.GetAsync(cancellationToken);
        if (branchList.Count == 0)
            throw new KeyNotFoundException("The brranch list is empty.");

        return _mapper.Map<List<ListBranchResult>>(branchList);
    }

}
