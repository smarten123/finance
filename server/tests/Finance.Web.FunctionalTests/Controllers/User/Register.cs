using Finance.Web.Models.User;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Finance.Web.FunctionalTests.Controllers.User
{
    public class Register : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public Register(CustomWebApplicationFactory fixture)
        {
            _httpClient = fixture.CreateClient();
        }

        [Fact]
        public async Task Should_respond_with_no_content_response_when_new_user_is_registered()
        {
            // Arrange
            var viewModel = new RegisterViewModel
            {
                Email = "steve@gmail.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd"
            };
            var jsonToPost = JsonConvert.SerializeObject(viewModel);

            // Act
            var response = await _httpClient.PostAsync("/users",
                new StringContent(jsonToPost, Encoding.UTF8, MediaTypeNames.Application.Json));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Should_respond_with_email_required_message_when_received_email_field_is_empty()
        {
            // Arrange
            var viewModel = new RegisterViewModel
            {
                Email = "",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd"
            };
            var jsonToPost = JsonConvert.SerializeObject(viewModel);

            // Act
            var response = await _httpClient.PostAsync("/users",
                new StringContent(jsonToPost, Encoding.UTF8, MediaTypeNames.Application.Json));
            var responseStringContent = await response.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseStringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var emailErrors = responseContent["errors"]["Email"].Should().BeOfType<JArray>().Subject.ToObject<List<string>>();
            emailErrors.Should().Contain("The Email field is required.");

        }

        [Fact]
        public async Task Should_require_email_with_valid_format()
        {
            // Arrange
            var viewModel = new RegisterViewModel
            {
                Email = "jefferson.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd"
            };
            var jsonToPost = JsonConvert.SerializeObject(viewModel);

            // Act
            var response = await _httpClient.PostAsync("/users",
                new StringContent(jsonToPost, Encoding.UTF8, MediaTypeNames.Application.Json));
            var responseStringContent = await response.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseStringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var emailErrors = responseContent["errors"]["Email"].Should().BeOfType<JArray>().Subject.ToObject<List<string>>();
            emailErrors.Should().Contain("The Email field is not a valid e-mail address.");
        }

        [Fact]
        public async Task Should_require_password_field()
        {
            // Arrange
            var viewModel = new RegisterViewModel
            {
                Email = "ben_johnson@gmail.com",
                Password = "",
                ConfirmPassword = "P@ssw0rd"
            };
            var jsonToPost = JsonConvert.SerializeObject(viewModel);

            // Act
            var response = await _httpClient.PostAsync("/users",
                new StringContent(jsonToPost, Encoding.UTF8, MediaTypeNames.Application.Json));
            var responseStringContent = await response.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseStringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var passwordErrors = responseContent["errors"]["Password"].Should().BeOfType<JArray>().Subject.ToObject<List<string>>();
            passwordErrors.Should().Contain("The Password field is required.");
        }

        [Fact]
        public async Task Password_field_should_match_with_confirmPassword()
        {
            // Arrange
            var viewModel = new RegisterViewModel
            {
                Email = "ben_johnson@gmail.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "Passw0rd"
            };
            var jsonToPost = JsonConvert.SerializeObject(viewModel);

            // Act
            var response = await _httpClient.PostAsync("/users",
                new StringContent(jsonToPost, Encoding.UTF8, MediaTypeNames.Application.Json));
            var responseStringContent = await response.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseStringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var confirmPasswordErrors = responseContent["errors"]["ConfirmPassword"].Should().BeOfType<JArray>().Subject.ToObject<List<string>>();
            confirmPasswordErrors.Should().Contain("The Password field and ConfirmPassword field must match.");
        }

        [Fact]
        public async Task Should_not_have_less_than_six_characters_as_password_field()
        {
            // Arrange
            var viewModel = new RegisterViewModel
            {
                Email = "ben_johnson@gmail.com",
                Password = "P@sw0",
                ConfirmPassword = "P@sw0"
            };
            var jsonToPost = JsonConvert.SerializeObject(viewModel);

            // Act
            var response = await _httpClient.PostAsync("/users",
                new StringContent(jsonToPost, Encoding.UTF8, MediaTypeNames.Application.Json));
            var responseStringContent = await response.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseStringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var passwordErrors = responseContent["errors"]["Password"].Should().BeOfType<JArray>().Subject.ToObject<List<string>>();
            passwordErrors.Should().Contain("The Password field must be between 6 and 100 characters long.");
        }

        [Fact]
        public async Task Should_not_have_more_than_one_hundred_characters_as_password_field()
        {
            // Arrange
            var viewModel = new RegisterViewModel
            {
                Email = "ben_johnson@gmail.com",
                Password = "P@ssw0rdIMadeThisVeryLongTextAndItWillMakeSureThePasswordIsExactlyOneHundredAndOneCharactersBelieveMe",
                ConfirmPassword = "P@ssw0rdIMadeThisVeryLongTextAndItWillMakeSureThePasswordIsExactlyOneHundredAndOneCharactersBelieveMe"
            };
            var jsonToPost = JsonConvert.SerializeObject(viewModel);

            // Act
            var response = await _httpClient.PostAsync("/users",
                new StringContent(jsonToPost, Encoding.UTF8, MediaTypeNames.Application.Json));
            var responseStringContent = await response.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseStringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var passwordErrors = responseContent["errors"]["Password"].Should().BeOfType<JArray>().Subject.ToObject<List<string>>();
            passwordErrors.Should().Contain("The Password field must be between 6 and 100 characters long.");
        }

        [Fact]
        public async Task Should_respond_with_errors_that_occur_in_infrastructure_by_sending_duplicate_email()
        {
            // Arrange
            // This user account should already exist based on the on the seeded data set up in the CustomWebApplicationFactory ClassFixture
            var viewModel = new RegisterViewModel
            {
                Email = "stanford@gmail.com",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd"
            };
            var jsonToPost = JsonConvert.SerializeObject(viewModel);

            // Act
            var response = await _httpClient.PostAsync("/users",
                new StringContent(jsonToPost, Encoding.UTF8, MediaTypeNames.Application.Json));
            var responseStringContent = await response.Content.ReadAsStringAsync();
            var responseContent = JObject.Parse(responseStringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var passwordErrors = responseContent["errors"]["DuplicateUserName"].Should().BeOfType<JArray>().Subject.ToObject<List<string>>();
            passwordErrors.Should().Contain("User name 'stanford@gmail.com' is already taken.");
        }
    }
}
