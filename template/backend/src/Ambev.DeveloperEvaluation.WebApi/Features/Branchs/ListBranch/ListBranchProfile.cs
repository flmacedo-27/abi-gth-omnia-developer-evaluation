using Ambev.DeveloperEvaluation.Application.Branchs.ListBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.ListBranch;

/// <summary>
/// Profile for mapping ListBranch feature requests to commands
/// </summary>
public class ListBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListBranch feature
    /// </summary>
    public ListBranchProfile()
    {
        CreateMap<Guid, ListBranchCommand>()
            .ConstructUsing(src => new ListBranchCommand());
    }
}