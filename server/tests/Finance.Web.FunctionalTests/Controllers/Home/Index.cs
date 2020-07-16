using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Finance.Web.FunctionalTests.Controllers.Home
{
    public class Index : IClassFixture<CustomWebApplicationFactoryFixture>
    {
        private readonly HttpClient _httpClient;

        public Index(CustomWebApplicationFactoryFixture factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Should_respond_with_swagger_docs_on_redirect()
        {
            // Act
            var swaggerPage = await _httpClient.GetAsync("/");
            swaggerPage.EnsureSuccessStatusCode();

            // Assert
            swaggerPage.RequestMessage.RequestUri.Should().Be(_httpClient.BaseAddress + "swagger/index.html");
        }
    }
}
