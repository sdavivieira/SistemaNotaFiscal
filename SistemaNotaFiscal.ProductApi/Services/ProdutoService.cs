using SistemaNotaFiscal.ProductApi.Model;
using SistemaNotaFiscal.ProductApi.Repositories.Interfaces;
using SistemaNotaFiscal.ProductApi.Services.Interfaces;
using System.Net.Http;

namespace SistemaNotaFiscal.ProductApi.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProductRepository _repository;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public ProdutoService(IProductRepository productRepository)
        {
            _repository = productRepository;
        }
        public async Task<Produto> CadastrarProduto(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentException("Produto não pode ser nulo");
            }

            if (produto.Saldo <= 0)
            {
                throw new ArgumentException("O saldo do produto deve ser maior que zero.");
            }

            var produtoCriado = await _repository.CadastrarProduto(produto);

            if (produtoCriado == null)
            {
                throw new InvalidOperationException("Não foi possível cadastrar o produto.");
            }

            return produtoCriado;
        }

        public async Task<bool> AtualizarSaldoProduto(int produtoId, int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("A quantidade deve ser maior que zero.");
            }

            await _semaphore.WaitAsync();
            try
            {
                var produto = await _repository.ObterProdutoPorId(produtoId);
                if (produto == null)
                {
                    throw new InvalidOperationException("Produto não encontrado.");
                }

                if (produto.Saldo < quantidade)
                {
                    throw new InvalidOperationException("Saldo insuficiente para realizar a operação.");
                }

                produto.Saldo -= quantidade;
                var sucesso = await _repository.AtualizarSaldoProduto(produto);

                return sucesso;
            }
            finally
            {
                _semaphore.Release(); 
            }
        }

        public async Task<Produto> ObterProdutoPorId(int produtoId)
        {
            if (produtoId <= 0)
            {
                throw new ArgumentException("O ID do produto deve ser maior que zero.");
            }

            var produto = await _repository.ObterProdutoPorId(produtoId);

            return produto ?? throw new InvalidOperationException("Produto não encontrado.");
        }
    }
}
