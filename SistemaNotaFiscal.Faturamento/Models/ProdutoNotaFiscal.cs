using SistemaNotaFiscal.ProductApi.Model;

namespace SistemaNotaFiscal.Faturamento.Models
{
    public class ProdutoNotaFiscal
    {

        public int Id { get; set; }
        public int Quantidade { get; set; }
        public Produto Produtos { get; set; }  
    }
}
