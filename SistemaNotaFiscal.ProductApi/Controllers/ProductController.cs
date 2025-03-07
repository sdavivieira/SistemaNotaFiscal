using Microsoft.AspNetCore.Mvc;
using SistemaNotaFiscal.ProductApi.Model;
using SistemaNotaFiscal.ProductApi.Services;
using SistemaNotaFiscal.ProductApi.Services.Interfaces;

namespace SistemaNotaFiscal.ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProductController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarProduto([FromBody] Produto produto)
        {
            try
            {
                var produtoCriado = await _produtoService.CadastrarProduto(produto);
                return Ok(produtoCriado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro inesperado. Tente novamente.");
            }
        }

        [HttpPut("{produtoId}/atualizarSaldo")]
        public async Task<IActionResult> AtualizarSaldoProduto(int produtoId, [FromBody] int quantidade)
        {
            if (quantidade <= 0)
            {
                return BadRequest("A quantidade deve ser maior que zero.");
            }

            try
            {
                var produtoAtualizado = await _produtoService.AtualizarSaldoProduto(produtoId, quantidade);

                if (produtoAtualizado)
                {
                    return Ok("Saldo do produto atualizado com sucesso.");
                }
                else
                {
                    return NotFound("Produto não encontrado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar saldo do produto: " + ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoPorId(int id)
        {
            try
            {
                var produto = await _produtoService.ObterProdutoPorId(id);

                if (produto == null)
                {
                    return NotFound("Produto não encontrado.");
                }

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao obter dados do produto: " + ex.Message);
            }
        }

    }

}
