using SistemaNotaFiscal.ProductApi.Model;

namespace SistemaNotaFiscal.ProductApi.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<bool> AtualizarSaldoProduto(int produtoId, int quantidade);
        Task<Produto> CadastrarProduto(Produto product);
        Task<Produto> ObterProdutoPorId(int produtoId);
    }
}
