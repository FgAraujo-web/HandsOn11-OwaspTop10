using HandsOn11.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace HandsOn11.Api.Data;
public static class SeedData
{
    public static async Task InitializeAsync(LabDbContext db)
    {
        if (await db.Produtos.AnyAsync()) return;
        db.Produtos.AddRange(
            new Produto { Id = 1, Nome = "Teclado", Categoria = "Perifericos", Preco = 35 },
            new Produto { Id = 2, Nome = "Rato", Categoria = "Perifericos", Preco = 18 },
            new Produto { Id = 3, Nome = "Monitor", Categoria = "Ecras", Preco = 189 },
            new Produto { Id = 4, Nome = "Portatil", Categoria = "Computadores", Preco = 850 });
        await db.SaveChangesAsync();
    }
}
