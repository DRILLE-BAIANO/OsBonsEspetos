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
                Itens = carrinho
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Adicionar(int produtoId, string nome, decimal preco)
        {
            var carrinho = ObterCarrinhoDaSession();
            
            var itemExistente = carrinho.FirstOrDefault(item => item.ProdutoId == produtoId);
            
            if (itemExistente != null)
            {
                itemExistente.Quantidade++;
            }
            else
            {
                carrinho.Add(new ItemCarrinho
                {
                    ProdutoId = produtoId,
                    Produto = new Produto { Id = produtoId, Nome = nome, Preco = preco },
                    Quantidade = 1,
                    PrecoUnitario = preco
                });
            }

            SalvarCarrinhoNaSession(carrinho);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            var carrinho = ObterCarrinhoDaSession();
            var item = carrinho.FirstOrDefault(item => item.ProdutoId == id);
            
            if (item != null)
            {
                carrinho.Remove(item);
                SalvarCarrinhoNaSession(carrinho);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AtualizarQuantidade(int produtoId, int quantidade)
        {
            var carrinho = ObterCarrinhoDaSession();
            var item = carrinho.FirstOrDefault(item => item.ProdutoId == produtoId);
            
            if (item != null && quantidade > 0)
            {
                item.Quantidade = quantidade;
                SalvarCarrinhoNaSession(carrinho);
            }

            return RedirectToAction("Index");
        }

        private List<ItemCarrinho> ObterCarrinhoDaSession()
        {
            var carrinhoJson = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(carrinhoJson))
                return new List<ItemCarrinho>();

            return JsonSerializer.Deserialize<List<ItemCarrinho>>(carrinhoJson) ?? new List<ItemCarrinho>();
        }

        private void SalvarCarrinhoNaSession(List<ItemCarrinho> carrinho)
        {
            var carrinhoJson = JsonSerializer.Serialize(carrinho);
            HttpContext.Session.SetString(SessionKey, carrinhoJson);
        }
    }
}