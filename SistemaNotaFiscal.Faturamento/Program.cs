using SistemaNotaFiscal.Faturamento.Services.Interfaces;
using SistemaNotaFiscal.Faturamento.Services;
using Microsoft.EntityFrameworkCore;
using SistemaNotaFiscal.Faturamento.Repositories.Interfaces;
using SistemaNotaFiscal.Faturamento.Repositories;
using SistemaNotaFiscal.Faturamento.Context;
using SistemaNotaFiscal.ProductApi.Services.Interfaces;
using SistemaNotaFiscal.ProductApi.Services;
using SistemaNotaFiscal.ProductApi.Repositories.Interfaces;
using SistemaNotaFiscal.ProductApi.Repositories;
using SistemaNotaFiscal.ProductApi.Context;
using Microsoft.OpenApi.Models; // Adicione esta linha

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Faturamento API", Version = "v1" });

    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (apiDesc.RelativePath.StartsWith("api/produtos", StringComparison.OrdinalIgnoreCase))
            return false; 
        return true;
    });
});
builder.Services.AddHttpClient<SistemaNotaFiscal.Faturamento.Services.Interfaces.IProdutoService, SistemaNotaFiscal.Faturamento.Services.ProdutoService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7295/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddDbContext<AppNFDbContext>(options =>
    options.UseInMemoryDatabase("SistemaNotaFiscalDb"));

builder.Services.AddDbContext<AppProductDbContext>(options =>
    options.UseInMemoryDatabase("SistemaNotaFiscalProductDb"));

builder.Services.AddScoped<INotaFiscalService, NotaFiscalService>();
builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();

builder.Services.AddScoped<SistemaNotaFiscal.ProductApi.Services.Interfaces.IProdutoService, SistemaNotaFiscal.ProductApi.Services.ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();