// ViewModels/CarrinhoViewModel.cs
using OsBonsEspetos.Models;
using System.Collections.Generic;
using System.Linq;

namespace OsBonsEspetos.ViewModels
{
    public class CarrinhoViewModel
    {
    public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
        public decimal TotalCarrinho => Itens.Sum(item => item.Subtotal);
    }
    
    public class CarrinhoItemViewModel
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = new Produto();
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}