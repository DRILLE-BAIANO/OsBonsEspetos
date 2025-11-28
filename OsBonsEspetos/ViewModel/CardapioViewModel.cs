// ViewModels/CardapioViewModel.cs
using OsBonsEspetos.Models;
using System.Collections.Generic;

namespace OsBonsEspetos.ViewModels
{
    public class CardapioViewModel
    {
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public List<string> Categorias { get; set; } = new List<string>();
        public string CategoriaSelecionada { get; set; } = string.Empty;
    }
}