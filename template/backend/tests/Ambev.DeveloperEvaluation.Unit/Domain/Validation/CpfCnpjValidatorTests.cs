using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="CpfCnpjValidator"/> class.
/// Tests validation of CPF/CNPJ numbers according to the following rules:
/// - Must not be empty
/// - Must be a valid CPF (11 digits) or CNPJ (14 digits)
/// </summary>
public class CpfCnpjValidatorTests
{
    [Theory(DisplayName = "Given a CPF/CNPJ When validating Then should validate according to length and format")]
    [InlineData("123.456.789-09", true)]      // Valid CPF with formatting
    [InlineData("12345678909", true)]         // Valid CPF without formatting
    [InlineData("12.345.678/0001-95", true)] // Valid CNPJ with formatting
    [InlineData("12345678000195", true)]      // Valid CNPJ without formatting
    [InlineData("123.456.789-0", false)]      // Invalid CPF (too short)
    [InlineData("1234567890", false)]         // Invalid CPF (too short)
    [InlineData("12.345.678/0001-9", false)] // Invalid CNPJ (too short)
    [InlineData("1234567800019", false)]      // Invalid CNPJ (too short)
    [InlineData("123.456.789-091", false)]   // Invalid CPF (too long)
    [InlineData("123456789091", false)]       // Invalid CPF (too long)
    [InlineData("12.345.678/0001-951", false)] // Invalid CNPJ (too long)
    [InlineData("123456780001951", false)]     // Invalid CNPJ (too long)
    [InlineData("abc.def.ghi-jk", false)]     // Invalid CPF (contains letters)
    [InlineData("ab.cde.fgh/ijkl-mn", false)] // Invalid CNPJ (contains letters)
    [InlineData("", false)]                   // Invalid - empty
    [InlineData("   ", false)]                // Invalid - whitespace
    public void Given_CpfCnpj_When_Validating_Then_ShouldValidateAccordingToLengthAndFormat(string cpfCnpj, bool expectedResult)
    {
        // Arrange
        var validator = new CpfCnpjValidator();

        // Act
        var result = validator.Validate(cpfCnpj);

        // Assert
        result.IsValid.Should().Be(expectedResult);
    }
}