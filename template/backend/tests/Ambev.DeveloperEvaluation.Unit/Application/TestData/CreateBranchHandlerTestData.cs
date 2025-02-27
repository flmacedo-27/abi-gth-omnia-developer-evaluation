using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateBranchHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Branch entities.
    /// The generated branches will have valid:
    /// - Name (using company names)
    /// - Code (unique branch code)
    /// - City (valid city names)
    /// - State (valid state abbreviations)
    /// - Country (valid country names)
    /// - PostalCode (valid postal codes)
    /// - Phone (valid phone numbers)
    /// - Email (valid email addresses)
    /// - Status (Active or Inactive)
    /// </summary>
    private static readonly Faker<CreateBranchCommand> createBranchHandlerFaker = new Faker<CreateBranchCommand>()
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Code, f => f.Random.AlphaNumeric(10))
        .RuleFor(b => b.City, f => f.Address.City())
        .RuleFor(b => b.State, f => f.Address.StateAbbr())
        .RuleFor(b => b.Country, f => f.Address.Country())
        .RuleFor(b => b.PostalCode, f => f.Address.ZipCode("#####-###"))
        .RuleFor(b => b.Phone, f => f.Phone.PhoneNumber("(##) #####-####"))
        .RuleFor(b => b.Email, f => f.Internet.Email());
    
    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static CreateBranchCommand GenerateValidCommand()
    {
        return createBranchHandlerFaker.Generate();
    }
}