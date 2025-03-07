using SistemaNotaFiscal.Faturamento.Models;

namespace SistemaNotaFiscal.Faturamento.Repositories.Interfaces
{
    public interface INotaFiscalRepository
    {
        Task AtualizarNotaFiscal(NotaFiscal notaFiscal);
        Task<NotaFiscal> CadastrarNotaFiscal(NotaFiscal notaFiscal);
        Task<NotaFiscal> ObterNotaFiscalPorId(int notaFiscalId);
    }
}
