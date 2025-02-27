using Ambev.DeveloperEvaluation.Application.Branchs.ListBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branchs;

public class ListBranchHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenBranchListIsNotEmpty()
    {
        // Arrange
        var mockRepository = new Mock<IBranchRepository>();
        var mockMapper = new Mock<IMapper>();

        var branchList = new List<Branch>
        {
            new Branch { Id = Guid.NewGuid(), Name = "Branch 1" },
            new Branch { Id = Guid.NewGuid(), Name = "Branch 2" },
            new Branch { Id = Guid.NewGuid(), Name = "Branch 3" }
        };

        var branchListResult = new List<ListBranchResult>
        {
            new ListBranchResult { Id = Guid.NewGuid(), Name = "Branch 1" },
            new ListBranchResult { Id = Guid.NewGuid(), Name = "Branch 2" },
            new ListBranchResult { Id = Guid.NewGuid(), Name = "Branch 3" }
        };

        mockRepository
            .Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(branchList);

        mockMapper
            .Setup(mapper => mapper.Map<List<ListBranchResult>>(It.IsAny<List<Branch>>()))
            .Returns(branchListResult);

        var handler = new ListBranchHandler(mockRepository.Object, mockMapper.Object);

        var request = new ListBranchCommand
        {
            PageNumber = 1,
            PageSize = 2
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal(2, result.TotalPages);
        Assert.Equal(3, result.TotalCount);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenBranchListIsEmpty()
    {
        // Arrange
        var mockRepository = new Mock<IBranchRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository
            .Setup(repo => repo.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Branch>());

        var handler = new ListBranchHandler(mockRepository.Object, mockMapper.Object);

        var request = new ListBranchCommand
        {
            PageNumber = 1,
            PageSize = 10
        };

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None)
        );
    }
}