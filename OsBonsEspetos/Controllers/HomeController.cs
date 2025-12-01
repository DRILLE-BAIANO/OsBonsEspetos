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
                new Produto { 
                    Id = 1, 
                    Nome = "Espeto de Carne", 
                    Preco = 7.50m, 
                    Descricao = "Espeto de Carne suculenta com temperos especiais",
                    Categoria = "espetos",  // ADICIONADO: Categoria do produto
                    ImagemUrl = "/images/espetos/carne.png"  // ADICIONADO: URL da imagem
                },
                new Produto { 
                    Id = 2, 
                    Nome = "Espeto de Frango", 
                    Preco = 7.50m, 
                    Descricao = "Espeto de Frango marinado e grelhado",
                    Categoria = "espetos",  // ADICIONADO: Categoria do produto
                    ImagemUrl = "/images/espetos/frango.png"  // ADICIONADO: URL da imagem
                },
                new Produto { 
                    Id = 2, 
                    Nome = "Espeto de Queijo", 
                    Preco = 7.00m, 
                    Descricao = "Espeto de queijo coalho grelhado",
                    Categoria = "espetos",  // ADICIONADO: Categoria do produto
                    ImagemUrl = "/images/espetos/queijo.png"  // ADICIONADO: URL da imagem
                },
                new Produto { 
                    Id = 3, 
                    Nome = "Refrigerante", 
                    Preco = 8.00m, 
                    Descricao = "Refrigerante gelado 350ml",
                    Categoria = "bebidas",  // ADICIONADO: Categoria do produto
                    ImagemUrl = "/images/bebidas/refrigerantes.png"  // ADICIONADO: URL da imagem
                },
                // Adicione mais produtos com categorias diferentes para testar o filtro
                new Produto { 
                    Id = 5, 
                    Nome = "Espeto de Romeu e Julieta", 
                    Preco = 8.50m, 
                    Descricao = "Espeto com queijo coalho e goiabada",
                    Categoria = "sobremesas",  // ADICIONADO
                    ImagemUrl = "/images/sobremesas/queijo-com-goiabada.png"
                },

                new Produto { 
                    Id = 7, 
                    Nome = "Suco Natural", 
                    Preco = 12.00m, 
                    Descricao = "Suco natural de laranja",
                    Categoria = "bebidas",  // ADICIONADO
                    ImagemUrl = "/images/bebidas/suco.png"
                },

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