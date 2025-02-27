using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class ListProductHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenProductListIsNotEmpty()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMapper>();

        var productList = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 10.0m },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 20.0m }
            };

        var productListResult = new List<ListProductResult>
            {
                new ListProductResult { Id = productList[0].Id, Name = "Product 1", Price = 10.0m },
                new ListProductResult { Id = productList[1].Id, Name = "Product 2", Price = 20.0m }
            };

        mockRepository
            .Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(productList);

        mockMapper
            .Setup(mapper => mapper.Map<List<ListProductResult>>(productList))
            .Returns(productListResult);

        var handler = new ListProductHandler(mockRepository.Object, mockMapper.Object);

        var request = new ListProductCommand { PageNumber = 1, PageSize = 10 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(2, result.TotalCount);

        mockRepository.Verify(repo => repo.GetAsync(It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<List<ListProductResult>>(productList), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenProductListIsEmpty()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository
            .Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Product>());

        var handler = new ListProductHandler(mockRepository.Object, mockMapper.Object);

        var request = new ListProductCommand { PageNumber = 1, PageSize = 10 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("The product list is empty.", exception.Message);
        mockRepository.Verify(repo => repo.GetAsync(It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<List<ListProductResult>>(It.IsAny<List<Product>>()), Times.Never);
    }
}