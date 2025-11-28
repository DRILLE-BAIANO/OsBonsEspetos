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
                new Produto { Id = 1, Nome = "Espeto de Carne", Preco = 25.90m, Descricao = "Espeto de Carne suculenta com temperos especiais", Categoria = "Espetos" },
                new Produto { Id = 2, Nome = "Espeto de Frango", Preco = 22.50m, Descricao = "Espeto de Frango marinado e grelhado", Categoria = "Espetos" },
                new Produto { Id = 3, Nome = "Refrigerante", Preco = 8.00m, Descricao = "Refrigerante gelado 350ml", Categoria = "Bebidas" },
                new Produto { Id = 4, Nome = "Batata Frita", Preco = 12.00m, Descricao = "Porção de batata frita crocante", Categoria = "Acompanhamentos" }
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