using Microsoft.EntityFrameworkCore;
using SistemaNotaFiscal.ProductApi.Context;
using SistemaNotaFiscal.ProductApi.Services.Interfaces;
using SistemaNotaFiscal.ProductApi.Services;
using SistemaNotaFiscal.ProductApi.Repositories.Interfaces;
using SistemaNotaFiscal.ProductApi.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controle de Estoque API", Version = "v1" });
});

builder.Services.AddDbContext<AppProductDbContext>(options =>
    options.UseInMemoryDatabase("SistemaNotaFiscalDb"));

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

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
