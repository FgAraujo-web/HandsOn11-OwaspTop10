using HandsOn11.Api.Data;
using HandsOn11.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
var provider = builder.Configuration["Database:Provider"] ?? "Sqlite";
var connectionString = provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase)
    ? builder.Configuration.GetConnectionString("SqlServer")
    : builder.Configuration.GetConnectionString("Sqlite");
if (string.IsNullOrWhiteSpace(connectionString)) throw new InvalidOperationException("Connection string em falta.");
builder.Services.AddDbContext<LabDbContext>(options =>
{
    if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase)) options.UseSqlServer(connectionString);
    else if (provider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase)) options.UseSqlite(connectionString);
    else throw new InvalidOperationException("Provider suportado: Sqlite ou SqlServer.");
});
builder.Services.AddScoped<ProdutoService>();
var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();
app.UseHttpsRedirection();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LabDbContext>();
    await db.Database.EnsureCreatedAsync();
    await SeedData.InitializeAsync(db);
}
app.Run();
public partial class Program { }
