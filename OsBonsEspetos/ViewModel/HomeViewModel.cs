// ViewModels/HomeViewModel.cs
using OsBonsEspetos.Models;
using System.Collections.Generic;

namespace OsBonsEspetos.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Produto> Produtos { get; set; } = new List<Produto>();
    public IEnumerable<Categoria> Categorias { get; set; } = new List<Categoria>();
}
