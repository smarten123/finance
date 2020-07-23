using System.Threading.Tasks;
using Finance.Application.Actions.User.Register;
using Finance.Application.Common.Interfaces;
using Finance.Application.Common.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Finance.Application.UnitTests.Actions.User.Register
{
    public class RegisterUserCommandHandlerTests
    {
        [Fact]
        public async Task Should_pass_user_registration_result_from_user_manager_to_caller()
        {
            // Act
            var expectedResult = Result.Success();

            var userManagerMock = new Mock<IUserManager>();
            userManagerMock
                .Setup(mock => mock.CreateAsync("john@doe.com", "P@ssw0rd"))
                .ReturnsAsync(expectedResult);

            var handler = new RegisterUserCommandHandler(userManagerMock.Object);

            // Act
            var actualResult = await handler.Handle(new RegisterUserCommand("john@doe.com", "P@ssw0rd"));

            // Assert
            actualResult.Result.Should().Be(expectedResult);
        }
    }
}
