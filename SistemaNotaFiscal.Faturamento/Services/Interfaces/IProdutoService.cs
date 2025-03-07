using SistemaNotaFiscal.ProductApi.Model;

namespace SistemaNotaFiscal.Faturamento.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<Produto> ObterProdutoPorId(int produtoId);
        Task<bool> AtualizarSaldoProduto(int produtoId, int quantidade);
    }


}
