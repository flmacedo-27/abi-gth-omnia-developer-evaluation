using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateBranchHandler"/> class.
/// </summary>
public class CreateBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly CreateBranchHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBranchHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateBranchHandler(_branchRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid branch creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid branch data When creating branch Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Code = command.Code,
            City = command.City,
            State = command.State,
            Country = command.Country,
            PostalCode = command.PostalCode,
            Phone = command.Phone,
            Email = command.Email
        };

        var result = new CreateBranchResult
        {
            Id = branch.Id,
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _mapper.Map<CreateBranchResult>(branch).Returns(result);

        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        // When
        var createBranchResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createBranchResult.Should().NotBeNull();
        createBranchResult.Id.Should().Be(branch.Id);
        await _branchRepository.Received(1).CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid branch creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid branch data When creating branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateBranchCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to branch entity")]
    public async Task Handle_ValidRequest_MapsCommandToBranch()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Code = command.Code,
            City = command.City,
            State = command.State,
            Country = command.Country,
            PostalCode = command.PostalCode,
            Phone = command.Phone,
            Email = command.Email
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>())
            .Returns(branch);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Branch>(Arg.Is<CreateBranchCommand>(c =>
            c.Name == command.Name &&
            c.Code == command.Code &&
            c.City == command.City &&
            c.State == command.State &&
            c.Country == command.Country &&
            c.PostalCode == command.PostalCode &&
            c.Phone == command.Phone &&
            c.Email == command.Email));
    }
}