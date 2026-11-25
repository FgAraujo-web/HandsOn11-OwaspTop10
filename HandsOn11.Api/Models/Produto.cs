namespace HandsOn11.Api.Models;
public sealed class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}
