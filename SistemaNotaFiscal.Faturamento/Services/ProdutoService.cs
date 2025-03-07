using SistemaNotaFiscal.Faturamento.Services.Interfaces;
using SistemaNotaFiscal.ProductApi.Model;

namespace SistemaNotaFiscal.Faturamento.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _httpClient;

        public ProdutoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Produto> ObterProdutoPorId(int produtoId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7295/api/Produtos/{produtoId}");
            if (response.IsSuccessStatusCode)
            {
                var produto = await response.Content.ReadFromJsonAsync<Produto>();
                return produto;
            }
            return null;
        }

        public async Task<bool> AtualizarSaldoProduto(int produtoId, int quantidade)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7295/api/Produtos/{produtoId}/atualizarSaldo", quantidade);
            return response.IsSuccessStatusCode;
        }


    }

}
