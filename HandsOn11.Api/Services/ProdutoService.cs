using HandsOn11.Api.Data;
using HandsOn11.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace HandsOn11.Api.Services;
public sealed class ProdutoService(LabDbContext db)
{
    public Task<List<Produto>> PesquisarSeguroAsync(string categoria, CancellationToken ct) =>
        db.Produtos.AsNoTracking().Where(p => p.Categoria == categoria).OrderBy(p => p.Id).ToListAsync(ct);

    // EXCLUSIVAMENTE PARA DEMONSTRAÇÃO LOCAL: concatenação deliberadamente insegura.
    // Nunca copiar este padrão para código de produção.
    public Task<List<Produto>> PesquisarVulneravelAsync(string categoria, CancellationToken ct)
    {
        var sql = $"SELECT Id, Nome, Categoria, Preco FROM Produtos WHERE Categoria = '{categoria}'";
        return db.Produtos.FromSqlRaw(sql).AsNoTracking().ToListAsync(ct);
    }
}
