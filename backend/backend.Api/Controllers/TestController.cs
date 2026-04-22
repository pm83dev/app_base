using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class TestController : ControllerBase
{
    [HttpGet("status")]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "ok, server is running",
            serverTime = DateTime.UtcNow
        });
    }
}
