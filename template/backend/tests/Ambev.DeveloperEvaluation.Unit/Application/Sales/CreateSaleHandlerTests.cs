using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateSaleCommand> _validator;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _branchRepository = Substitute.For<IBranchRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = new CreateSaleValidator(_customerRepository, _branchRepository, _productRepository);
        _handler = new CreateSaleHandler(_saleRepository, _validator, _mapper);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleFromCommand(command);

        SetupRepositories(command, sale);

        var result = new CreateSaleResult { Id = sale.Id };
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
    public async Task Handle_ValidRequest_MapsCommandToSale()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = CreateSaleFromCommand(command);

        SetupRepositories(command, sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
            c.Number == command.Number &&
            c.Date == command.Date &&
            c.CustomerId == command.CustomerId &&
            c.BranchId == command.BranchId &&
            c.Items.Count == command.Items.Count &&
            c.TotalSaleAmount == command.TotalSaleAmount));
    }

    private Sale CreateSaleFromCommand(CreateSaleCommand command)
    {
        var validatorSale = new SaleValidator(_customerRepository, _branchRepository, _productRepository);
        var validatorSaleItem = new SaleItemValidator(_productRepository);

        return new Sale(validatorSale)
        {
            Id = Guid.NewGuid(),
            Number = command.Number,
            Date = command.Date,
            CustomerId = command.CustomerId,
            BranchId = command.BranchId,
            Items = command.Items.Select(item => new SaleItem(validatorSaleItem)
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrices = item.UnitPrices,
                Discount = item.Discount,
                TotalSaleItemAmount = item.TotalSaleItemAmount
            }).ToList(),
            TotalSaleAmount = command.TotalSaleAmount
        };
    }

    private void SetupRepositories(CreateSaleCommand command, Sale sale)
    {
        var customer = new Customer
        {
            Id = command.CustomerId,
            Fullname = "Fullname Teste",
            CpfCnpj = "123.456.789-09",
            Email = "fullname.teste@example.com",
            Phone = "(11) 98765-4321",
            Status = CustomerStatus.Active
        };
        _customerRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(customer);

        var branch = new Branch
        {
            Id = command.BranchId,
            Name = "Name Teste",
            Code = "12345",
            City = "Salvador",
            State = "Bahia",
            Country = "Brasil",
            PostalCode = "12345678",
            Phone = "(11) 98765-4321",
            Email = "fullname.teste@example.com",
            Status = BranchStatus.Active
        };
        _branchRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        var product = new Product
        {
            Id = command.Items.Select(i => i.ProductId).FirstOrDefault(),
            Name = "Name Teste",
            Code = "123454",
            Description = string.Empty,
            Price = 24.0m,
            Status = ProductStatus.Active
        };
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(product);

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);
    }
}