using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="DateValidator"/> class.
/// Tests validation of dates according to the following rules:
/// - Must not be empty or default (e.g., DateTime.MinValue)
/// - Must be a valid date (not in the future)
/// </summary>
public class DateValidatorTests
{
    [Theory(DisplayName = "Given a date When validating Then should validate according to rules")]
    [InlineData("2023-10-01", true)]          // Valid date (past date)
    [InlineData("2023-02-29", false)]         // Invalid date (non-existent date)
    [InlineData("2023-04-31", false)]         // Invalid date (non-existent date)
    [InlineData("0001-01-01", false)]         // Invalid date (default/min value)
    public void Given_Date_When_Validating_Then_ShouldValidateAccordingToRules(string dateString, bool expectedResult)
    {
        // Arrange
        var validator = new DateValidator();
        bool isDateValid = DateTime.TryParse(dateString, out DateTime date);

        if (!isDateValid)
        {
            expectedResult = false;
            date = DateTime.MinValue;
        }

        // Act
        var result = validator.Validate(date);

        // Assert
        result.IsValid.Should().Be(expectedResult);
    }

    [Fact(DisplayName = "Given a default date When validating Then should return invalid")]
    public void Given_DefaultDate_When_Validating_Then_ShouldReturnInvalid()
    {
        // Arrange
        var validator = new DateValidator();
        DateTime defaultDate = default;

        // Act
        var result = validator.Validate(defaultDate);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact(DisplayName = "Given today's date When validating Then should return valid")]
    public void Given_TodaysDate_When_Validating_Then_ShouldReturnValid()
    {
        // Arrange
        var validator = new DateValidator();
        DateTime today = DateTime.Today;

        // Act
        var result = validator.Validate(today);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact(DisplayName = "Given a future date When validating Then should return invalid")]
    public void Given_FutureDate_When_Validating_Then_ShouldReturnInvalid()
    {
        // Arrange
        var validator = new DateValidator();
        DateTime futureDate = DateTime.Now.AddDays(1);

        // Act
        var result = validator.Validate(futureDate);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
