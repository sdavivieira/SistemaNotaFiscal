using Microsoft.AspNetCore.Mvc;
using SistemaNotaFiscal.Faturamento.Models;
using SistemaNotaFiscal.Faturamento.Services.Interfaces;

namespace SistemaNotaFiscal.Faturamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalService _notaFiscalService;

        public NotaFiscalController(INotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarNotaFiscal([FromBody] NotaFiscal notaFiscal)
        {
            try
            {
                var notaFiscalCriada = await _notaFiscalService.CadastrarNotaFiscal(notaFiscal);
                return Ok(notaFiscalCriada);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("imprimir/{notaFiscalId}")]
        public async Task<IActionResult> ImprimirNotaFiscal(int notaFiscalId)
        {
            try
            {
                var sucesso = await _notaFiscalService.ImprimirNotaFiscal(notaFiscalId);

                if (sucesso)
                {
                    return Ok(new { message = "Nota Fiscal Impressa com Sucesso!" });
                }
                else
                {
                    return BadRequest("Falha ao imprimir a Nota Fiscal.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
