﻿using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customers;

/// <summary>
/// Contains unit tests for the <see cref="CreateCustomerHandler"/> class.
/// </summary>
public class CreateCustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly CreateCustomerHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCustomerHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateCustomerHandlerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCustomerHandler(_customerRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid customer creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid customer data When creating customer Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateCustomerHandlerTestData.GenerateValidCommand();
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Fullname = command.Fullname,
            CpfCnpj = command.CpfCnpj,
            Email = command.Email,
            Phone = command.Phone
        };

        var result = new CreateCustomerResult
        {
            Id = customer.Id,
        };

        _mapper.Map<Customer>(command).Returns(customer);
        _mapper.Map<CreateCustomerResult>(customer).Returns(result);

        _customerRepository.CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(customer);

        // When
        var createCustomerResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createCustomerResult.Should().NotBeNull();
        createCustomerResult.Id.Should().Be(customer.Id);
        await _customerRepository.Received(1).CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid customer creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid customer data When creating customer Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCustomerCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to customer entity")]
    public async Task Handle_ValidRequest_MapsCommandToCustomer()
    {
        // Given
        var command = CreateCustomerHandlerTestData.GenerateValidCommand();
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Fullname = command.Fullname,
            CpfCnpj = command.CpfCnpj,
            Email = command.Email,
            Phone = command.Phone
        };

        _mapper.Map<Customer>(command).Returns(customer);
        _customerRepository.CreateAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(customer);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Customer>(Arg.Is<CreateCustomerCommand>(c =>
            c.Fullname == command.Fullname &&
            c.CpfCnpj == command.CpfCnpj &&
            c.Email == command.Email &&
            c.Phone == command.Phone));
    }
}