using Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branchs;

public class DeleteBranchHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenBranchIsDeleted()
    {
        // Arrange
        var mockRepository = new Mock<IBranchRepository>();
        var handler = new DeleteBranchHandler(mockRepository.Object);

        var request = new DeleteBranchCommand(Guid.NewGuid());

        mockRepository
            .Setup(repo => repo.DeleteAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        mockRepository.Verify(repo => repo.DeleteAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenBranchNotFound()
    {
        // Arrange
        var mockRepository = new Mock<IBranchRepository>();
        var handler = new DeleteBranchHandler(mockRepository.Object);

        var request = new DeleteBranchCommand(Guid.NewGuid());

        mockRepository
            .Setup(repo => repo.DeleteAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal($"Branch with ID {request.Id} not found", exception.Message);
        mockRepository.Verify(repo => repo.DeleteAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<IBranchRepository>();
        var handler = new DeleteBranchHandler(mockRepository.Object);

        var request = new DeleteBranchCommand(Guid.Empty);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("Branch ID is required", exception.Errors.First().ErrorMessage);
        mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}