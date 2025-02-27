using Ambev.DeveloperEvaluation.Application.Customers.ListCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customers;

public class ListCustomerHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenCustomerListIsNotEmpty()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        var mockMapper = new Mock<IMapper>();

        var customerList = new List<Customer>
        {
            new Customer { Id = Guid.NewGuid(), Fullname = "Full name 1" },
            new Customer { Id = Guid.NewGuid(), Fullname = "Full name 2" }
        };

        var customerListResult = new List<ListCustomerResult>
        {
            new ListCustomerResult { Id = customerList[0].Id, Fullname = "Full name 1" },
            new ListCustomerResult { Id = customerList[1].Id, Fullname = "Full name 2" }
        };

        mockRepository
            .Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(customerList);

        mockMapper
            .Setup(mapper => mapper.Map<List<ListCustomerResult>>(customerList))
            .Returns(customerListResult);

        var handler = new ListCustomerHandler(mockRepository.Object, mockMapper.Object);

        var request = new ListCustomerCommand { PageNumber = 1, PageSize = 10 };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(2, result.TotalCount);

        mockRepository.Verify(repo => repo.GetAsync(It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<List<ListCustomerResult>>(customerList), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenCustomerListIsEmpty()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository
            .Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Customer>());

        var handler = new ListCustomerHandler(mockRepository.Object, mockMapper.Object);

        var request = new ListCustomerCommand { PageNumber = 1, PageSize = 10 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("The customer list is empty.", exception.Message);
        mockRepository.Verify(repo => repo.GetAsync(It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<List<ListCustomerResult>>(It.IsAny<List<Customer>>()), Times.Never);
    }
}