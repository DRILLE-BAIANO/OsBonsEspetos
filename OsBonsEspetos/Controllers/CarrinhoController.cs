// Controllers/CarrinhoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OsBonsEspetos.Models;
using OsBonsEspetos.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace OsBonsEspetos.Controllers
{
    public class CarrinhoController : Controller
    {
        private const string SessionKey = "Carrinho";

        public IActionResult Index()
        {
            var carrinho = ObterCarrinhoDaSession();
            var viewModel = new CarrinhoViewModel
            {
                Itens = carrinho.Select(item => new CarrinhoItemViewModel
                {
                    ProdutoId = item.ProdutoId,
                    Produto = item.Produto,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] AdicionarAoCarrinhoRequest request)
        {
            try
            {
                if (request.ProdutoId <= 0 || request.Quantidade <= 0)
                {
                    return Json(new { success = false, message = "Dados inválidos" });
                }

                var carrinho = ObterCarrinhoDaSession();

                var itemExistente = carrinho.FirstOrDefault(item => item.ProdutoId == request.ProdutoId);

                if (itemExistente != null)
                {
                    itemExistente.Quantidade += request.Quantidade;
                }
                else
                {
                    carrinho.Add(new ItemCarrinho
                    {
                        ProdutoId = request.ProdutoId,
                        Produto = new Produto
                        {
                            Id = request.ProdutoId,
                            Nome = request.Nome ?? $"Produto {request.ProdutoId}",
                            Preco = request.Preco
                        },
                        Quantidade = request.Quantidade,
                        PrecoUnitario = request.Preco
                    });
                }

                SalvarCarrinhoNaSession(carrinho);

                var carrinhoAtualizado = ObterCarrinhoDaSession();
                var totalItens = carrinhoAtualizado.Sum(item => item.Quantidade);

                return Json(new
                {
                    success = true,
                    message = "Produto adicionado ao carrinho!",
                    totalItens = totalItens
                });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = $"Erro: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Adicionar()
        {
            try
            {
                Console.WriteLine("=== MÉTODO ADICIONAR CHAMADO ===");

                // Ler os dados do FormData
                if (!int.TryParse(HttpContext.Request.Form["produtoId"], out int produtoId))
                {
                    Console.WriteLine("Erro: produtoId inválido");
                    return Json(new { success = false, message = "ID do produto inválido" });
                }

                if (!int.TryParse(HttpContext.Request.Form["quantidade"], out int quantidade))
                {
                    quantidade = 1; // Valor padrão
                }

                Console.WriteLine($"ProdutoId: {produtoId}, Quantidade: {quantidade}");

                if (produtoId <= 0 || quantidade <= 0)
                {
                    return Json(new { success = false, message = "Dados inválidos" });
                }

                var carrinho = ObterCarrinhoDaSession();
                Console.WriteLine($"Carrinho antes: {carrinho.Count} itens");

                var itemExistente = carrinho.FirstOrDefault(item => item.ProdutoId == produtoId);

                if (itemExistente != null)
                {
                    itemExistente.Quantidade += quantidade;
                    Console.WriteLine($"Item existente atualizado: {itemExistente.Produto.Nome} - Quantidade: {itemExistente.Quantidade}");
                }
                else
                {
                    // Criar produto com base no ID
                    var produto = CriarProduto(produtoId);

                    carrinho.Add(new ItemCarrinho
                    {
                        ProdutoId = produtoId,
                        Produto = produto,
                        Quantidade = quantidade,
                        PrecoUnitario = produto.Preco
                    });
                    Console.WriteLine($"Novo item adicionado: {produto.Nome}");
                }

                SalvarCarrinhoNaSession(carrinho);

                var carrinhoAtualizado = ObterCarrinhoDaSession();
                var totalItens = carrinhoAtualizado.Sum(item => item.Quantidade);

                Console.WriteLine($"Carrinho depois: {carrinhoAtualizado.Count} itens, Total: {totalItens} itens");

                return Json(new
                {
                    success = true,
                    message = "Produto adicionado ao carrinho!",
                    totalItens = totalItens
                });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"ERRO NO MÉTODO ADICIONAR: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return Json(new { success = false, message = $"Erro: {ex.Message}" });
            }
        }

        // Método auxiliar para criar produtos de exemplo
        private Produto CriarProduto(int produtoId)
        {
            return produtoId switch
            {
                1 => new Produto { Id = 1, Nome = "Espeto de Carne", Preco = 25.90m, Categoria = "espetos" },
                2 => new Produto { Id = 2, Nome = "Espeto de Frango", Preco = 22.50m, Categoria = "espetos" },
                3 => new Produto { Id = 3, Nome = "Refrigerante", Preco = 8.00m, Categoria = "bebidas" },
                _ => new Produto { Id = produtoId, Nome = $"Produto {produtoId}", Preco = 10.00m, Categoria = "outros" }
            };
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            try
            {
                var carrinho = ObterCarrinhoDaSession();
                var item = carrinho.FirstOrDefault(item => item.ProdutoId == id);

                if (item != null)
                {
                    carrinho.Remove(item);
                    SalvarCarrinhoNaSession(carrinho);
                    TempData["Success"] = "Produto removido do carrinho!";
                }
                else
                {
                    TempData["Error"] = "Produto não encontrado no carrinho";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = $"Erro ao remover produto: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult AtualizarQuantidade([FromBody] AtualizarQuantidadeRequest request)
        {
            try
            {
                if (request.ProdutoId <= 0 || request.Quantidade <= 0)
                {
                    return Json(new { success = false, message = "Dados inválidos" });
                }

                var carrinho = ObterCarrinhoDaSession();
                var item = carrinho.FirstOrDefault(item => item.ProdutoId == request.ProdutoId);

                if (item != null)
                {
                    item.Quantidade = request.Quantidade;
                    SalvarCarrinhoNaSession(carrinho);

                    var carrinhoAtualizado = ObterCarrinhoDaSession();
                    var itemAtualizado = carrinhoAtualizado.FirstOrDefault(i => i.ProdutoId == request.ProdutoId);
                    var totalCarrinho = carrinhoAtualizado.Sum(i => i.Quantidade * i.PrecoUnitario);

                    return Json(new
                    {
                        success = true,
                        novaQuantidade = itemAtualizado?.Quantidade ?? 0,
                        novoSubtotal = (itemAtualizado?.Quantidade ?? 0) * (itemAtualizado?.PrecoUnitario ?? 0),
                        totalCarrinho = totalCarrinho
                    });
                }

                return Json(new { success = false, message = "Produto não encontrado no carrinho" });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = $"Erro: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Limpar()
        {
            try
            {
                HttpContext.Session.Remove(SessionKey);
                TempData["Success"] = "Carrinho limpo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = $"Erro ao limpar carrinho: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult ObterContador()
        {
            var carrinho = ObterCarrinhoDaSession();
            var totalItens = carrinho.Sum(item => item.Quantidade);

            return Json(new { totalItens = totalItens });
        }

        private List<ItemCarrinho> ObterCarrinhoDaSession()
        {
            try
            {
                var carrinhoJson = HttpContext.Session.GetString(SessionKey);
                if (string.IsNullOrEmpty(carrinhoJson))
                    return new List<ItemCarrinho>();

                return JsonSerializer.Deserialize<List<ItemCarrinho>>(carrinhoJson) ?? new List<ItemCarrinho>();
            }
            catch
            {
                return new List<ItemCarrinho>();
            }
        }

        private void SalvarCarrinhoNaSession(List<ItemCarrinho> carrinho)
        {
            try
            {
                var carrinhoJson = JsonSerializer.Serialize(carrinho);
                HttpContext.Session.SetString(SessionKey, carrinhoJson);
            }
            catch
            {
                // Log do erro em produção
            }
        }
    }

    // Classes de request
    public class AdicionarAoCarrinhoRequest
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; } = 1;
    }

    public class AtualizarQuantidadeRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}