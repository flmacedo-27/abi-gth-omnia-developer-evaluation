using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branchs.ListBranch;

/// <summary>
/// Profile for mapping between Branch entity and ListBranchResponse
/// </summary>
public class ListBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListBranch operation
    /// </summary>
    public ListBranchProfile()
    {
        CreateMap<Branch, ListBranchResult>();
    }
}