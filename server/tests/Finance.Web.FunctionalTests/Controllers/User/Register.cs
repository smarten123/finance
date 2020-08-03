using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Finance.Web.Models.User;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Finance.Web.FunctionalTests.Controllers.User
{
    public class Register : IClassFixture<CustomWebApplicationFactoryFixture>
    {
        private readonly HttpClient _httpClient;

        public Register(CustomWebApplicationFactoryFixture fixture)
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
    }
}
