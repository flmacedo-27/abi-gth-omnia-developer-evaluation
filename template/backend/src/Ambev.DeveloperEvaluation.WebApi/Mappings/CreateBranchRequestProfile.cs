﻿using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class CreateBranchRequestProfile : Profile
{
    public CreateBranchRequestProfile()
    {
        CreateMap<CreateBranchRequest, CreateBranchCommand>();
    }
}