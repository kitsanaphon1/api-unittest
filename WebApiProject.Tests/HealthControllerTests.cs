using Xunit;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Controllers;

namespace WebApiProject.Tests
{
    public class HealthControllerTests
    {
        [Fact]
        public void GetHealth_ReturnsOk()
        {
            var controller = new HealthController();
            var result = controller.GetHealth();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Healthy ✅", okResult.Value);
        }
    }
}
