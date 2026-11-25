using System.Net;
using System.Net.Http.Json;
using HandsOn11.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
namespace HandsOn11.Tests;
public sealed class SegurancaTests
{
    [Fact]
    public async Task Health_Publico_Devolve200()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/health")).StatusCode);
    }

    [Fact]
    public async Task PesquisaSegura_CategoriaValida_DevolveDoisProdutos()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        var data = await client.GetFromJsonAsync<List<Produto>>("/api/produtos/seguro?categoria=Perifericos");
        Assert.Equal(2, data!.Count);
    }

    [Fact]
    public async Task PesquisaSegura_PayloadSqlInjection_NaoContornaFiltro()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        var payload = Uri.EscapeDataString("' OR 1=1 --");
        var data = await client.GetFromJsonAsync<List<Produto>>("/api/produtos/seguro?categoria=" + payload);
        Assert.Empty(data!);
    }

    [Fact]
    public async Task LaboratorioVulneravel_PayloadDemonstraFalha()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        var payload = Uri.EscapeDataString("' OR 1=1 --");
        var data = await client.GetFromJsonAsync<List<Produto>>("/api/produtos/lab/vulneravel?categoria=" + payload);
        Assert.Equal(4, data!.Count);
    }

    [Fact]
    public async Task PesquisaSegura_SemCategoria_Devolve400()
    {
        using var factory = new ApiFactory();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.BadRequest, (await client.GetAsync("/api/produtos/seguro")).StatusCode);
    }

    [Fact]
    public async Task LaboratorioDesativado_Devolve404()
    {
        using var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Production");
            builder.ConfigureAppConfiguration((_, config) => config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Database:Provider"] = "Sqlite",
                ["ConnectionStrings:Sqlite"] = "Data Source=:memory:",
                ["Lab:EnableVulnerableEndpoint"] = "true"
            }));
        });
        // Verifica apenas a regra de acesso ao laboratório em Production.
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync("/api/produtos/lab/vulneravel?categoria=Perifericos")).StatusCode);
    }
}
