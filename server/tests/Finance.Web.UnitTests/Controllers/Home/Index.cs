using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Finance.Web.UnitTests.Controllers.Home
{
    public class Index
    {
        [Fact]
        public void Should_redirect_to_swagger_docs()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var response = controller.Index();

            // Assert
            response.Should().BeOfType<RedirectResult>().Which.Url.Should().Be("~/swagger");
        }
    }
}
