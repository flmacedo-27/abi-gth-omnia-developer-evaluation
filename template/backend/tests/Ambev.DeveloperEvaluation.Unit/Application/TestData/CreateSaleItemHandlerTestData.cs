using Ambev.DeveloperEvaluation.Application.SalesItem.CreateSalesItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleItemHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid SaleItem entities.
    /// The generated sale items will have valid:
    /// - ProductId (valid GUID for product)
    /// - Quantity (greater than 0)
    /// - UnitPrices (greater than 0)
    /// - Discount (optional, but greater than 0 when quantity is above 4)
    /// - TotalSaleItemAmount (greater than 0)
    /// </summary>
    private static readonly Faker<CreateSaleItemCommand> createSaleItemHandlerFaker = new Faker<CreateSaleItemCommand>()
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrices, f => f.Random.Decimal(10, 100))
        .RuleFor(i => i.Discount, (f, i) => i.Quantity > 4 ? f.Random.Decimal(0.1m, 0.2m) : null)
        .RuleFor(i => i.TotalSaleItemAmount, (f, i) => i.Quantity * i.UnitPrices * (1 - (i.Discount ?? 0)));

    /// <summary>
    /// Generates a valid SaleItem entity with randomized data.
    /// The generated sale item will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid SaleItem entity with randomly generated data.</returns>
    public static CreateSaleItemCommand GenerateValidCommand()
    {
        return createSaleItemHandlerFaker.Generate();
    }
}