using HandsOn11.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace HandsOn11.Api.Controllers;
[ApiController]
[Route("api/produtos")]
public sealed class ProdutosController(ProdutoService service, IConfiguration config, IWebHostEnvironment env) : ControllerBase
{
    [HttpGet("seguro")]
    public async Task<IActionResult> Seguro([FromQuery] string? categoria, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(categoria)) return BadRequest(new { erro = "Indique a categoria." });
        return Ok(await service.PesquisarSeguroAsync(categoria, ct));
    }

    [HttpGet("lab/vulneravel")]
    public async Task<IActionResult> Vulneravel([FromQuery] string? categoria, CancellationToken ct)
    {
        if (!env.IsDevelopment() || !config.GetValue<bool>("Lab:EnableVulnerableEndpoint")) return NotFound();
        if (string.IsNullOrWhiteSpace(categoria)) return BadRequest(new { erro = "Indique a categoria." });
        return Ok(await service.PesquisarVulneravelAsync(categoria, ct));
    }
}
