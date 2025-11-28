// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using OsBonsEspetos.Models;
using OsBonsEspetos.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

namespace OsBonsEspetos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Exemplo de produtos - substitua pelos seus dados reais
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Espeto de Carne", Preco = 25.90m, Descricao = "Espeto de Carne suculenta com temperos especiais" },
                new Produto { Id = 2, Nome = "Espeto de Frango", Preco = 22.50m, Descricao = "Espeto de Frango marinado e grelhado" },
                new Produto { Id = 3, Nome = "Refrigerante", Preco = 8.00m, Descricao = "Refrigerante gelado 350ml" }
            };

            var viewModel = new HomeViewModel
            {
                Produtos = produtos
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}