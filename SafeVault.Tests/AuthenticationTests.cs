using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SafeVault.Controllers;

namespace SafeVault.Tests
{

    [TestFixture]
    public class RoleAuthorizationTests
    {
        [Test]
        public void AdminDashboard_ShouldRequireAdminRole()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object);
            var mockHttpContext = new Mock<HttpContext>();
            var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Admin")
        };
            var identity = new System.Security.Claims.ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(identity);

            mockHttpContext.Setup(c => c.User).Returns(claimsPrincipal);

            var controllerContext = new ControllerContext() { HttpContext = mockHttpContext.Object };
            controller.ControllerContext = controllerContext;

            var result = controller.Admin() as ViewResult;

            Assert.That(result != null, "Result should not be null");
            Assert.That(result?.ViewName == "Admin", "View name should be Admin");
        }

        [Test]
        public void UserDashboard_ShouldRequireUserRole()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object);
            var mockHttpContext = new Mock<HttpContext>();
            var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User")
        };
            var identity = new System.Security.Claims.ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(identity);

            mockHttpContext.Setup(c => c.User).Returns(claimsPrincipal);

            var controllerContext = new ControllerContext() { HttpContext = mockHttpContext.Object };
            controller.ControllerContext = controllerContext;

            var result = controller.User() as ViewResult;

            Assert.That(result != null, "Result should not be null");
            Assert.That(result?.ViewName == "User", "View name should be User");
        }
    }
}