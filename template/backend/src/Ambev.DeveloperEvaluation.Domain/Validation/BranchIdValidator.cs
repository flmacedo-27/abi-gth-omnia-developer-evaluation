using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class BranchIdValidator : AbstractValidator<Guid>
{
    private readonly IBranchRepository _branchRepository;

    public BranchIdValidator(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;

        RuleFor(branchId => branchId)
            .NotEmpty().WithMessage("The branchId is required.")
            .MustAsync(BeValidBranchId).WithMessage("The BranchId must be a valid ID that exists in the branch table.");
    }

    private async Task<bool> BeValidBranchId(Guid branchId, CancellationToken cancellationToken)
    {
        Branch? branch = await _branchRepository.GetByIdAsync(branchId, cancellationToken);

        return branch != null;
    }
}