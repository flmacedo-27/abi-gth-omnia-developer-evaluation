using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.ListBranch;

public record ListBranchCommand : IRequest<List<ListBranchResult>>
{
}
