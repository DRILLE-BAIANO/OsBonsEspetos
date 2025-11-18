using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsBonsEspetos.Models;

public class ItemCarrinho
{
    public int Id { get; set; }
    public int CarrinhoId { get; set; }
    public Carrinho Carrinho { get; set; } // Propriedade de navegação
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } // Propriedade de navegação
    public int Quantidade { get; set; }
}

