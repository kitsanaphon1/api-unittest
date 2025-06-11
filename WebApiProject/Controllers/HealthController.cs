using Microsoft.AspNetCore.Mvc;

namespace WebApiProject.Controllers
{
    [Route("api/health")]
[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHealth()
    {
        return Ok("Healthy ✅");
    }
}
}
