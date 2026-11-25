using Microsoft.AspNetCore.Mvc;
namespace HandsOn11.Api.Controllers;
[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "OK", projeto = "HANDS-ON #11 | OWASP Top 10 - SQL Injection" });
}
