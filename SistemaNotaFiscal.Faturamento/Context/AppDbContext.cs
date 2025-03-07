using Microsoft.EntityFrameworkCore;
using SistemaNotaFiscal.Faturamento.Models;
using SistemaNotaFiscal.ProductApi.Model;

namespace SistemaNotaFiscal.Faturamento.Context
{
    public class AppNFDbContext : DbContext
    {
        public AppNFDbContext(DbContextOptions<AppNFDbContext> options) : base(options) { }

        public DbSet<NotaFiscal> NotasFiscais { get; set; }
    }
}
