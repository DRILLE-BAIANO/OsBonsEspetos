using OsBonsEspetos.Models;
using System.Collections.Generic;
using System.Linq;

namespace OsBonsEspetos.ViewModels;

public class CarrinhoViewModel
{
    public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    
    // Propriedade calculada que soma o subtotal de todos os itens
    public decimal TotalCarrinho => Itens.Sum(i => i.Subtotal);
}
