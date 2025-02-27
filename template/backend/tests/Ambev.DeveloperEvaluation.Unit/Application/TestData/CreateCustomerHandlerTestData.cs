using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateCustomerHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated customers will have valid:
    /// - Fullname (using person names)
    /// - CpfCnpj (valid CPF or CNPJ format)
    /// - Email (valid email format)
    /// - Phone (valid phone number format)
    /// - Status (Active or Inactive)
    /// </summary>
    private static readonly Faker<CreateCustomerCommand> createCustomerHandlerFaker = new Faker<CreateCustomerCommand>()
        .RuleFor(c => c.Fullname, f => f.Name.FullName())
        .RuleFor(c => c.CpfCnpj, f => f.PickRandom(GenerateCpf(), GenerateCnpj()))
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("(##) #####-####"));

    /// <summary>
    /// Generates a valid CPF (Brazilian individual taxpayer registry).
    /// </summary>
    /// <returns>A valid CPF as a string.</returns>
    private static string GenerateCpf()
    {
        return new Faker().Random.ReplaceNumbers("###.###.###-##");
    }

    /// <summary>
    /// Generates a valid CNPJ (Brazilian company taxpayer registry).
    /// </summary>
    /// <returns>A valid CNPJ as a string.</returns>
    private static string GenerateCnpj()
    {
        return new Faker().Random.ReplaceNumbers("##.###.###/####-##");
    }

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static CreateCustomerCommand GenerateValidCommand()
    {
        return createCustomerHandlerFaker.Generate();
    }
}