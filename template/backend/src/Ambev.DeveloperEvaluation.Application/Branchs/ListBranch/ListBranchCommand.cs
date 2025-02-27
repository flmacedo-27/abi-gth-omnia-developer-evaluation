using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.ListBranch;

public record ListBranchCommand : IRequest<PaginatedList<ListBranchResult>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
