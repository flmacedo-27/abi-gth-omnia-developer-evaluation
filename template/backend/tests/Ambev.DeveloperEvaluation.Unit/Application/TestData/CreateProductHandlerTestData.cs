using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateProductHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated products will have valid:
    /// - Name (using product names)
    /// - Code (unique product code)
    /// - Description (optional product description)
    /// - Price (greater than 0)
    /// - Status (Active or Inactive)
    /// </summary>
    private static readonly Faker<CreateProductCommand> createProductHandlerFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Code, f => f.Random.AlphaNumeric(10))
        .RuleFor(p => p.Description, f => f.Lorem.Sentence())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000));

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static CreateProductCommand GenerateValidCommand()
    {
        return createProductHandlerFaker.Generate();
    }
}