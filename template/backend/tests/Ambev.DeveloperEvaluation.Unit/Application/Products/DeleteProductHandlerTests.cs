using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class DeleteProductHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenProductIsDeleted()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var handler = new DeleteProductHandler(mockRepository.Object);

        var productId = Guid.NewGuid();
        var request = new DeleteProductCommand(productId);

        mockRepository
            .Setup(repo => repo.DeleteAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        mockRepository.Verify(repo => repo.DeleteAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenProductNotFound()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var handler = new DeleteProductHandler(mockRepository.Object);

        var productId = Guid.NewGuid();
        var request = new DeleteProductCommand(productId);

        mockRepository
            .Setup(repo => repo.DeleteAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal($"Product with ID {productId} not found", exception.Message);
        mockRepository.Verify(repo => repo.DeleteAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var handler = new DeleteProductHandler(mockRepository.Object);

        var request = new DeleteProductCommand(Guid.Empty);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("Product ID is required", exception.Errors.First().ErrorMessage);
        mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}