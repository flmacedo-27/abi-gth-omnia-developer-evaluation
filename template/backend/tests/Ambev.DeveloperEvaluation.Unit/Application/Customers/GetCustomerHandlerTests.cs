using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customers;

public class GetCustomerHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenCustomerExists()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        var mockMapper = new Mock<IMapper>();

        var customerId = Guid.NewGuid();
        var customer = new Customer { Id = customerId, Fullname = "Full name 1" };
        var customerResult = new GetCustomerResult { Id = customerId, Fullname = "Full name 2" };

        mockRepository
            .Setup(repo => repo.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        mockMapper
            .Setup(mapper => mapper.Map<GetCustomerResult>(customer))
            .Returns(customerResult);

        var handler = new GetCustomerHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetCustomerCommand(customerId);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerId, result.Id);
        Assert.Equal("Full name 2", result.Fullname);

        mockRepository.Verify(repo => repo.GetByIdAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<GetCustomerResult>(customer), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenCustomerDoesNotExist()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        var mockMapper = new Mock<IMapper>();

        var customerId = Guid.NewGuid();

        mockRepository
            .Setup(repo => repo.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer)null);

        var handler = new GetCustomerHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetCustomerCommand(customerId);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal($"Customer with ID {customerId} not found", exception.Message);
        mockRepository.Verify(repo => repo.GetByIdAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
        mockMapper.Verify(mapper => mapper.Map<GetCustomerResult>(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        var mockMapper = new Mock<IMapper>();

        var handler = new GetCustomerHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetCustomerCommand(Guid.Empty);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("Customer ID is required", exception.Errors.First().ErrorMessage);
        mockRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        mockMapper.Verify(mapper => mapper.Map<GetCustomerResult>(It.IsAny<Customer>()), Times.Never);
    }
}