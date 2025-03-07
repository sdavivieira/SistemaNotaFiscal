using Microsoft.EntityFrameworkCore;
using SistemaNotaFiscal.ProductApi.Model;

namespace SistemaNotaFiscal.ProductApi.Context
{
    public class AppProductDbContext : DbContext
    {
        public AppProductDbContext(DbContextOptions<AppProductDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }    
    }
}
