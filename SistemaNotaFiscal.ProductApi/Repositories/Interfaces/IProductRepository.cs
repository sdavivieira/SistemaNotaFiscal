using SistemaNotaFiscal.ProductApi.Model;

namespace SistemaNotaFiscal.ProductApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Produto> CadastrarProduto(Produto produto);
        Task<bool> AtualizarSaldoProduto(Produto produto);
        Task<Produto> ObterProdutoPorId(int produtoId);
    }
}
