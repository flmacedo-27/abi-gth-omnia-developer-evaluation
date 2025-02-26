using Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.DeleteBranch;

/// <summary>
/// Profile for mapping DeleteBranch feature requests to commands
/// </summary>
public class DeleteBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteBranch feature
    /// </summary>
    public DeleteBranchProfile()
    {
        CreateMap<Guid, DeleteBranchCommand>()
            .ConstructUsing(id => new DeleteBranchCommand(id));
    }
}