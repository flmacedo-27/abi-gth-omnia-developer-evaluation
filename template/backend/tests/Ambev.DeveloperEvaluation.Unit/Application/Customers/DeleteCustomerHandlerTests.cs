using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customers;

public class DeleteCustomerHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCustomerIsDeleted()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        var handler = new DeleteCustomerHandler(mockRepository.Object);

        var customerId = Guid.NewGuid();
        var request = new DeleteCustomerCommand(customerId);

        mockRepository
            .Setup(repo => repo.DeleteAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        mockRepository.Verify(repo => repo.DeleteAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenCustomerNotFound()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        var handler = new DeleteCustomerHandler(mockRepository.Object);

        var customerId = Guid.NewGuid();
        var request = new DeleteCustomerCommand(customerId);

        mockRepository
            .Setup(repo => repo.DeleteAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal($"Customer with ID {customerId} not found", exception.Message);
        mockRepository.Verify(repo => repo.DeleteAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<ICustomerRepository>();
        var handler = new DeleteCustomerHandler(mockRepository.Object);

        var request = new DeleteCustomerCommand(Guid.Empty);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            handler.Handle(request, CancellationToken.None)
        );

        Assert.Equal("Customer ID is required", exception.Errors.First().ErrorMessage);
        mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}