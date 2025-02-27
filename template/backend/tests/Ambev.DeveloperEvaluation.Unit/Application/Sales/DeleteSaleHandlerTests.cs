using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class DeleteSaleHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSaleIsDeleted()
    {
        // Arrange
        var mockRepository = new Mock<ISaleRepository>();
        var handler = new DeleteSaleHandler(mockRepository.Object);

        var saleId = Guid.NewGuid();
        var request = new DeleteSaleCommand(saleId);

        mockRepository
            .Setup(repo => repo.DeleteAsync(saleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        mockRepository.Verify(repo => repo.DeleteAsync(saleId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenSaleNotFound()
    {
        // Arrange
        var mockRepository = new Mock<ISaleRepository>();
        var handler = new DeleteSaleHandler(mockRepository.Object);

        var saleId = Guid.NewGuid();
        var request = new DeleteSaleCommand(saleId);

        mockRepository
            .Setup(repo => repo.DeleteAsync(saleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal($"Sale with ID {saleId} not found", exception.Message);
        mockRepository.Verify(repo => repo.DeleteAsync(saleId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<ISaleRepository>();
        var handler = new DeleteSaleHandler(mockRepository.Object);

        var request = new DeleteSaleCommand(Guid.Empty);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("Sale ID is required", exception.Errors.First().ErrorMessage);
        mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}