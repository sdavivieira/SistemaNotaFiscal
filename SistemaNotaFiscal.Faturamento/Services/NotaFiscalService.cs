using iTextSharp.text;
using iTextSharp.text.pdf;
using SistemaNotaFiscal.Faturamento.Models;
using SistemaNotaFiscal.Faturamento.Repositories.Interfaces;
using SistemaNotaFiscal.Faturamento.Services.Interfaces;
using IProdutoService = SistemaNotaFiscal.Faturamento.Services.Interfaces.IProdutoService;

namespace SistemaNotaFiscal.Faturamento.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IProdutoService _produtoService;

        public NotaFiscalService(INotaFiscalRepository notaFiscalRepository, IProdutoService produtoService)
        {
            _notaFiscalRepository = notaFiscalRepository;
            _produtoService = produtoService;
        }

        public async Task<NotaFiscal> CadastrarNotaFiscal(NotaFiscal notaFiscal)
        {
            foreach (var item in notaFiscal.Produtos)
            {
                var produto = await _produtoService.ObterProdutoPorId(item.Produtos.Id);
                if (produto == null)
                {
                    throw new InvalidOperationException($"Produto {item.Produtos.Id} não encontrado.");
                }

                bool saldoAtualizado = await _produtoService.AtualizarSaldoProduto(item.Produtos.Id, item.Quantidade);

                if (!saldoAtualizado)
                {
                    throw new InvalidOperationException($"Saldo insuficiente ou erro ao atualizar o produto {produto.Nome}.");
                }
            }

            return await _notaFiscalRepository.CadastrarNotaFiscal(notaFiscal);
        }

        public async Task<bool> ImprimirNotaFiscal(int notaFiscalId)
        {
            var notaFiscal = await _notaFiscalRepository.ObterNotaFiscalPorId(notaFiscalId);

            if (notaFiscal == null)
            {
                throw new InvalidOperationException("Nota fiscal não encontrada.");
            }

            if (notaFiscal.Status.ToUpper() != "ABERTA")
            {
                throw new InvalidOperationException("Só é possível imprimir notas fiscais abertas.");
            }

            notaFiscal.Status = "FECHADA";
            await _notaFiscalRepository.AtualizarNotaFiscal(notaFiscal);

            await GerarPDF(notaFiscal);

            return true;
        }
        private async Task GerarPDF(NotaFiscal notaFiscal)
        {
            var directory = "C:\\NotasFiscais";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document();
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                document.Add(new Paragraph($"Nota Fiscal: {notaFiscal.Numero}", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 14)));
                document.Add(new Paragraph($"Status: {notaFiscal.Status}"));
                document.Add(new Paragraph($"Data de Emissão: {DateTime.Now.ToString("dd/MM/yyyy")}\n"));

                foreach (var item in notaFiscal.Produtos)
                {
                    var produto = item.Produtos;
                    if (produto != null)
                    {
                        document.Add(new Paragraph($"Produto: {produto.Nome} - Quantidade: {item.Quantidade} - Preço: {produto.Preco:C}"));
                    }
                }

                document.Close();

                var filePath = Path.Combine(directory, $"{notaFiscal.Numero}_NotaFiscal.pdf");
                File.WriteAllBytes(filePath, ms.ToArray());
            }
        }



    }

}
