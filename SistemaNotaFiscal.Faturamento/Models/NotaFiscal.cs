namespace SistemaNotaFiscal.Faturamento.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public int Numero { get; set; } 
        public string Status { get; set; }  
        public List<ProdutoNotaFiscal> Produtos { get; set; }  
    }

}
