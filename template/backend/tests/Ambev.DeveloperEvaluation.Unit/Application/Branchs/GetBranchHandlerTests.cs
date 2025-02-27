using Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branchs;

public class GetBranchHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnBranch_WhenBranchExists()
    {
        // Arrange
        var mockRepository = new Mock<IBranchRepository>();
        var mockMapper = new Mock<IMapper>();

        var branchId = Guid.NewGuid();
        var branch = new Branch { Id = branchId, Name = "Branch 1" };
        var branchResult = new GetBranchResult { Id = branchId, Name = "Branch 1" };

        mockRepository
            .Setup(repo => repo.GetByIdAsync(branchId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(branch);

        mockMapper
            .Setup(mapper => mapper.Map<GetBranchResult>(branch))
            .Returns(branchResult);

        var handler = new GetBranchHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetBranchCommand(branchId);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(branchId, result.Id);
        Assert.Equal("Branch 1", result.Name);

        mockRepository.Verify(repo => repo.GetByIdAsync(branchId, It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<GetBranchResult>(branch), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenBranchDoesNotExist()
    {
        // Arrange
        var mockRepository = new Mock<IBranchRepository>();
        var mockMapper = new Mock<IMapper>();

        var branchId = Guid.NewGuid();

        mockRepository
            .Setup(repo => repo.GetByIdAsync(branchId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Branch)null);

        var handler = new GetBranchHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetBranchCommand(branchId);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal($"Branch with ID {branchId} not found", exception.Message);
        mockRepository.Verify(repo => repo.GetByIdAsync(branchId, It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<GetBranchResult>(It.IsAny<Branch>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<IBranchRepository>();
        var mockMapper = new Mock<IMapper>();

        var handler = new GetBranchHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetBranchCommand(Guid.Empty);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("Branch ID is required", exception.Errors.First().ErrorMessage);
        mockRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        mockMapper.Verify(mapper => mapper.Map<GetBranchResult>(It.IsAny<Branch>()), Times.Never);
    }
}
