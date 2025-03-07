namespace SistemaNotaFiscal.ProductApi.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Saldo { get; set; }
    }
}
