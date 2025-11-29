// ViewModels/CarrinhoViewModel.cs
using System.Collections.Generic;
using System.Linq;
using OsBonsEspetos.Models; // Adicione esta using directive

namespace OsBonsEspetos.ViewModels
{
    public class CarrinhoViewModel
    {
        public List<CarrinhoItemViewModel> Itens { get; set; } = new List<CarrinhoItemViewModel>();
        public decimal TotalCarrinho => Itens.Sum(item => item.Subtotal);
    }

    public class CarrinhoItemViewModel
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}