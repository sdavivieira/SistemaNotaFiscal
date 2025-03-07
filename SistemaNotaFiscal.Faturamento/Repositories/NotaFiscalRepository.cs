
using Microsoft.EntityFrameworkCore;
using SistemaNotaFiscal.Faturamento.Context;
using SistemaNotaFiscal.Faturamento.Models;
using SistemaNotaFiscal.Faturamento.Repositories.Interfaces;

namespace SistemaNotaFiscal.Faturamento.Repositories
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly AppNFDbContext _appDbContext;
        public NotaFiscalRepository(AppNFDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<NotaFiscal> CadastrarNotaFiscal(NotaFiscal notaFiscal)
        {
            _appDbContext.NotasFiscais.Add(notaFiscal);
            await _appDbContext.SaveChangesAsync();
            return notaFiscal;
        }

        public async Task<NotaFiscal> ObterNotaFiscalPorId(int notaFiscalId)
        {
            return await _appDbContext.NotasFiscais
                .Include(n => n.Produtos)  
                    .ThenInclude(pn => pn.Produtos)  
                .FirstOrDefaultAsync(n => n.Id == notaFiscalId);
        }


        public async Task AtualizarNotaFiscal(NotaFiscal notaFiscal)
        {
            // Verifica se a nota fiscal existe no banco
            var notaExistente = await _appDbContext.NotasFiscais
                                               .Include(n => n.Produtos)  
                                               .FirstOrDefaultAsync(n => n.Id == notaFiscal.Id);

            if (notaExistente == null)
            {
                throw new InvalidOperationException("Nota fiscal não encontrada.");
            }

            notaExistente.Status = notaFiscal.Status;
            notaExistente.Numero = notaFiscal.Numero;
            await _appDbContext.SaveChangesAsync();
        }
    }
}

