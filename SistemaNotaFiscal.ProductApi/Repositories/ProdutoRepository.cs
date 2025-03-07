using SistemaNotaFiscal.ProductApi.Context;
using SistemaNotaFiscal.ProductApi.Model;
using SistemaNotaFiscal.ProductApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SistemaNotaFiscal.ProductApi.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppProductDbContext _appDbContext;
        public ProdutoRepository(AppProductDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Produto> ObterProdutoPorId(int produtoId)
        {
            return await _appDbContext.Produtos
                .AsNoTracking() 
                .FirstOrDefaultAsync(p => p.Id == produtoId);  
        }

        public async Task<Produto> CadastrarProduto(Produto produto)
        {
            await _appDbContext.Produtos.AddAsync(produto);
            await _appDbContext.SaveChangesAsync();

            return produto;
        }

        public async Task<bool> AtualizarSaldoProduto(Produto produto)
        {
            _appDbContext.Produtos.Update(produto);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

    }
}
