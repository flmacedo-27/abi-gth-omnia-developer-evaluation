using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetProductHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMapper>();

        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Name = "Product 1", Price = 10.0m };
        var productResult = new GetProductResult { Id = productId, Name = "Product 2", Price = 20.0m };

        mockRepository
            .Setup(repo => repo.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        mockMapper
            .Setup(mapper => mapper.Map<GetProductResult>(product))
            .Returns(productResult);

        var handler = new GetProductHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetProductCommand(productId);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
        Assert.Equal("Product 2", result.Name);
        Assert.Equal(20.0m, result.Price);

        mockRepository.Verify(repo => repo.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<GetProductResult>(product), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenProductDoesNotExist()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMapper>();

        var productId = Guid.NewGuid();

        mockRepository
            .Setup(repo => repo.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product)null);

        var handler = new GetProductHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetProductCommand(productId);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal($"Product with ID {productId} not found", exception.Message);
        mockRepository.Verify(repo => repo.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<GetProductResult>(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMapper>();

        var handler = new GetProductHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetProductCommand(Guid.Empty);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("Product ID is required", exception.Errors.First().ErrorMessage);
        mockRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        mockMapper.Verify(mapper => mapper.Map<GetProductResult>(It.IsAny<Product>()), Times.Never);
    }
}