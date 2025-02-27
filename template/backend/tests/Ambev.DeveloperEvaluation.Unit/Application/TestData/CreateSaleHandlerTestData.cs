using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.SalesItem.CreateSalesItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - Number (unique sale number)
    /// - Date (valid sale date)
    /// - CustomerId (valid GUID for customer)
    /// - BranchId (valid GUID for branch)
    /// - Items (list of valid sale items)
    /// - TotalSaleAmount (greater than 0)
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.Number, f => f.Random.Int(1, 1000))
        .RuleFor(s => s.Date, f => f.Date.Recent())
        .RuleFor(s => s.CustomerId, f => f.Random.Guid())
        .RuleFor(s => s.BranchId, f => f.Random.Guid())
        .RuleFor(s => s.Items, GenerateSaleItems)
        .RuleFor(s => s.TotalSaleAmount, f => f.Random.Decimal(1, 1000));

    /// <summary>
    /// Generates a list of valid SaleItem entities.
    /// </summary>
    /// <param name="f">The Faker instance.</param>
    /// <returns>A list of valid SaleItem entities.</returns>
    private static List<CreateSaleItemCommand> GenerateSaleItems(Faker f)
    {
        return new Faker<CreateSaleItemCommand>()
            .RuleFor(i => i.ProductId, f => f.Random.Guid())
            .RuleFor(i => i.Quantity, f => f.Random.Int(5, 9))
            .RuleFor(i => i.UnitPrices, f => f.Random.Decimal(10, 100))
            .RuleFor(i => i.Discount, f => 0.10m)
            .RuleFor(i => i.TotalSaleItemAmount, (f, i) => 
            {
                decimal total = i.Quantity * i.UnitPrices;

                if (i.Discount.HasValue)
                {
                    total *= 1 - i.Discount.Value;
                }

                return total;
            })
            .Generate(f.Random.Int(1, 1));
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleHandlerFaker.Generate();
    }
}