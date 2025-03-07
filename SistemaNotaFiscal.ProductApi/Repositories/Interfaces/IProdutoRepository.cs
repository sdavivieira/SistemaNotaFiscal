using SistemaNotaFiscal.ProductApi.Model;

namespace SistemaNotaFiscal.ProductApi.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> CadastrarProduto(Produto produto);
        Task<bool> AtualizarSaldoProduto(Produto produto);
        Task<Produto> ObterProdutoPorId(int produtoId);
    }
}
