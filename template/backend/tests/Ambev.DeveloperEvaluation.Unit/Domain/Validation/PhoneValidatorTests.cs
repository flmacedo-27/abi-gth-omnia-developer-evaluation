using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    /// <summary>
    /// Contains unit tests for the <see cref="PhoneValidator"/> class.
    /// Tests validation of phone numbers according to the following rules:
    /// - Must not be empty
    /// - Must match pattern: ^\+?[1-9]\d{1,14}$
    ///   (Optional '+' prefix, first digit 1-9, followed by 1-14 digits)
    /// </summary>
    public class PhoneValidatorTests
    {
        [Theory(DisplayName = "Given a phone number When validating Then should validate according to regex pattern")]
        [InlineData("(11) 98765-4321", true)]     // Valid - format (XX) XXXXX-XXXX
        [InlineData("11999999999", true)]         // Valid - 11 digits
        [InlineData("+551199999999", true)]       // Valid - international format
        [InlineData("1234567890", true)]          // Valid - 10 digits
        [InlineData("+123456789012", true)]       // Valid - long international format
        [InlineData("(11) 9876-5432", true)]      // Valid - format (XX) XXXX-XXXX
        [InlineData("+0123456789", false)]        // Invalid - starts with 0 after +
        [InlineData("0123456789", false)]         // Invalid - starts with 0
        [InlineData("+", false)]                  // Invalid - only +
        [InlineData("+12345678901234567", false)] // Invalid - too long (>15 digits with +)
        [InlineData("12345678901234567", false)]  // Invalid - too long (>15 digits)
        [InlineData("abc12345678", false)]        // Invalid - contains letters
        [InlineData("12.34567890", false)]        // Invalid - contains special characters
        [InlineData("", false)]                   // Invalid - empty
        public void Given_PhoneNumber_When_Validating_Then_ShouldValidateAccordingToPattern(string phone, bool expectedResult)
        {
            // Arrange
            var validator = new PhoneValidator();

            // Act
            var result = validator.Validate(phone);

            // Assert
            result.IsValid.Should().Be(expectedResult);
        }
    }
}
