using HandsOn11.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace HandsOn11.Api.Data;
public sealed class LabDbContext(DbContextOptions<LabDbContext> options) : DbContext(options)
{
    public DbSet<Produto> Produtos => Set<Produto>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(e =>
        {
            e.ToTable("Produtos");
            e.HasKey(p => p.Id);
            e.Property(p => p.Nome).HasMaxLength(150).IsRequired();
            e.Property(p => p.Categoria).HasMaxLength(80).IsRequired();
            e.Property(p => p.Preco).HasPrecision(12, 2);
        });
    }
}
