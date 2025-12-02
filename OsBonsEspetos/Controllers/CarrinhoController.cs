using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OsBonsEspetos.Models;
using OsBonsEspetos.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System;

namespace OsBonsEspetos.Controllers
{
    public class CarrinhoController : Controller
    {
        private const string SessionKey = "Carrinho";

        // GET: /Carrinho
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

        // POST: /Carrinho/Adicionar
        [HttpPost]
        public IActionResult Adicionar()
        {
            try
            {
                if (!int.TryParse(HttpContext.Request.Form["produtoId"], out int produtoId))
                {
                    return Json(new { success = false, message = "ID do produto inválido." });
                }
                int quantidade = 1;
                var carrinho = ObterCarrinhoDaSession();
                var itemExistente = carrinho.FirstOrDefault(item => item.ProdutoId == produtoId);
                if (itemExistente != null)
                {
                    itemExistente.Quantidade += quantidade;
                }
                else
                {
                    var produto = ObterProdutoPorId(produtoId);
                    if (produto == null)
                    {
                        return Json(new { success = false, message = "Produto não encontrado." });
                    }
                    carrinho.Add(new ItemCarrinho
                    {
                        ProdutoId = produtoId,
                        Produto = produto,
                        Quantidade = quantidade,
                        PrecoUnitario = produto.Preco
                    });
                }
                SalvarCarrinhoNaSession(carrinho);
                var totalItens = carrinho.Sum(item => item.Quantidade);
                return Json(new { success = true, message = "Produto adicionado ao carrinho!", totalItens = totalItens });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro inesperado: {ex.Message}" });
            }
        }

        // =================================================================
        // MÉTODO REMOVER - NECESSÁRIO PARA O ERRO DESAPARECER
        // =================================================================
        [HttpPost]
        public IActionResult Remover(int id)
        {
            var carrinho = ObterCarrinhoDaSession();
            var itemParaRemover = carrinho.FirstOrDefault(i => i.ProdutoId == id);
            if (itemParaRemover != null)
            {
                carrinho.Remove(itemParaRemover);
                SalvarCarrinhoNaSession(carrinho);
            }
            // Para o botão "Remover" da linha, ele recarrega a página
            return RedirectToAction(nameof(Index));
        }

        // =================================================================
        // MÉTODO ATUALIZARQUANTIDADE - QUE USA O MÉTODO REMOVER
        // =================================================================
        [HttpPost]
        public IActionResult AtualizarQuantidade()
        {
            try
            {
                if (!int.TryParse(Request.Form["produtoId"], out int produtoId) ||
                    !int.TryParse(Request.Form["quantidade"], out int novaQuantidade))
                {
                    return Json(new { success = false, message = "Dados inválidos." });
                }

                // CORREÇÃO: Se a quantidade for 0, remove o item e redireciona a página inteira
                if (novaQuantidade <= 0)
                {
                    var carrinho = ObterCarrinhoDaSession();
                    var itemParaRemover = carrinho.FirstOrDefault(i => i.ProdutoId == produtoId);
                    if (itemParaRemover != null)
                    {
                        carrinho.Remove(itemParaRemover);
                        SalvarCarrinhoNaSession(carrinho);
                    }
                    // Retorna um JSON especial para o front-end saber que precisa recarregar
                    return Json(new { success = true, reloadPage = true });
                }

                var carrinhoAtual = ObterCarrinhoDaSession();
                var itemParaAtualizar = carrinhoAtual.FirstOrDefault(i => i.ProdutoId == produtoId);

                if (itemParaAtualizar != null)
                {
                    itemParaAtualizar.Quantidade = novaQuantidade;
                    SalvarCarrinhoNaSession(carrinhoAtual);

                    decimal novoSubtotal = itemParaAtualizar.Quantidade * itemParaAtualizar.PrecoUnitario;
                    decimal totalCarrinho = carrinhoAtual.Sum(item => item.Quantidade * item.PrecoUnitario);

                    return Json(new
                    {
                        success = true,
                        novaQuantidade = itemParaAtualizar.Quantidade,
                        novoSubtotal = novoSubtotal,
                        totalCarrinho = totalCarrinho,
                        reloadPage = false // Indica que não precisa recarregar
                    });
                }
                return Json(new { success = false, message = "Produto não encontrado." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro: {ex.Message}" });
            }
        }

        // --- MÉTODOS AUXILIARES ---

        private Produto ObterProdutoPorId(int produtoId)
        {
            var listaDeProdutos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Espeto de Carne", Preco = 7.50m, Categoria = "espetos" },
                new Produto { Id = 2, Nome = "Espeto de Frango", Preco = 7.50m, Categoria = "espetos" },
                new Produto { Id = 3, Nome = "Espeto de Queijo", Preco = 8.00m, Categoria = "espetos" },
                new Produto { Id = 4, Nome = "Refrigerante", Preco = 8.00m, Categoria = "bebidas" },
                new Produto { Id = 5, Nome = "Espeto de Romeu e Julieta", Preco = 8.50m, Categoria = "sobremesas" },
                new Produto { Id = 6, Nome = "Suco Natural", Preco = 12.00m, Categoria = "bebidas" },
            };
            return listaDeProdutos.FirstOrDefault(p => p.Id == produtoId);
        }

        private List<ItemCarrinho> ObterCarrinhoDaSession()
        {
            var carrinhoJson = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(carrinhoJson)) return new List<ItemCarrinho>();
            return JsonSerializer.Deserialize<List<ItemCarrinho>>(carrinhoJson) ?? new List<ItemCarrinho>();
        }

        private void SalvarCarrinhoNaSession(List<ItemCarrinho> carrinho)
        {
            var carrinhoJson = JsonSerializer.Serialize(carrinho);
            HttpContext.Session.SetString(SessionKey, carrinhoJson);
        }
    }
    // ... classes de request
}
