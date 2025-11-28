// ViewModels/HomeViewModel.cs
using OsBonsEspetos.Models;
using System.Collections.Generic;

namespace OsBonsEspetos.ViewModels
{
    public class HomeViewModel
    {
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public List<CarrinhoItemViewModel> ItensCarrinho { get; set; } = new List<CarrinhoItemViewModel>();
        public decimal TotalCarrinho => ItensCarrinho.Sum(item => item.Subtotal);
    }
}