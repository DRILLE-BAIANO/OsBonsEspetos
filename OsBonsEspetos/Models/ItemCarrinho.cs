// Models/ItemCarrinho.cs
namespace OsBonsEspetos.Models
{
    public class ItemCarrinho
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = new Produto();
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}