// Controllers/CardapioController.cs
using Microsoft.AspNetCore.Mvc;
using OsBonsEspetos.Models;
using OsBonsEspetos.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OsBonsEspetos.Controllers
{
    public class CardapioController : Controller
    {
        public IActionResult Index(string categoria = "")
        {
            // Exemplo de produtos - substitua pelos seus dados reais
            var produtos = new List<Produto>
            {
                 new Produto { Id = 1, Nome = "Espeto de Carne", Preco = 7.50m, Categoria = "espetos" },
                new Produto { Id = 2, Nome = "Espeto de Frango", Preco = 7.50m, Categoria = "espetos" },
                new Produto { Id = 3, Nome = "Espeto de Queijo", Preco = 8.00m, Categoria = "espetos" },
                new Produto { Id = 4, Nome = "Refrigerante", Preco = 8.00m, Categoria = "bebidas" },
                new Produto { Id = 5, Nome = "Espeto de Romeu e Julieta", Preco = 8.50m, Categoria = "sobremesas" },
                new Produto { Id = 6, Nome = "Suco Natural", Preco = 12.00m, Categoria = "bebidas" },
            };

            // Filtrar por categoria se especificada
            if (!string.IsNullOrEmpty(categoria))
            {
                produtos = produtos.Where(p => p.Categoria == categoria).ToList();
            }

            var viewModel = new CardapioViewModel
            {
                Produtos = produtos,
                Categorias = new List<string> { "Todos", "Espetos", "Bebidas", "Acompanhamentos" },
                CategoriaSelecionada = categoria
            };

            return View(viewModel);
        }
    }
}