using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Actions.User.Register;
using FluentAssertions;
using Xunit;

namespace Finance.Application.UnitTests.Actions.User.Register
{
    public class RegisterUserCommandValidatorTests
    {
        [Fact]
        public void Should_not_allow_null_email()
        {
            // Arrange
            var validator = new RegisterUserCommandValidator();
            var command = new RegisterUserCommand(null, "P@ssw0rd");
            
            // Act
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be("An email is required.");
        }

        [Fact]
        public void Should_expect_email_with_valid_format()
        {
            // Arrange
            var validator = new RegisterUserCommandValidator();
            var command = new RegisterUserCommand("wrongEmail.com", "P@ssw0rd");

            // Act
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be("An email with a correct format is required.");
        }

        [Fact]
        public void Should_not_allow_null_password()
        {
            // Arrange
            var validator = new RegisterUserCommandValidator();
            var command = new RegisterUserCommand("john@doe.com", null);

            // Act
            var validationResult = validator.Validate(command);

            // Assert
            validationResult.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be("A password is required.");
        }
    }
}
