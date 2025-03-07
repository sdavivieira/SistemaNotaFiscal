using SistemaNotaFiscal.Faturamento.Models;

namespace SistemaNotaFiscal.Faturamento.Services.Interfaces
{
    public interface INotaFiscalService
    {
        Task<NotaFiscal> CadastrarNotaFiscal(NotaFiscal notaFiscal);
        Task<bool> ImprimirNotaFiscal(int notaFiscalId);
    }
}
