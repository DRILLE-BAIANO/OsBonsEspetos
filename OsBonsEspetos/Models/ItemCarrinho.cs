// Models/ItemCarrinho.cs
using System; // Adicione esta using directive se necessário

namespace OsBonsEspetos.Models
{
    public class ItemCarrinho
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        
        // Propriedade calculada para facilitar
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}