// =================================================================
// Usings (Importações)
// =================================================================
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsBonsEspetos.Data;
using OsBonsEspetos.Models;
using OsBonsEspetos.ViewModels;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OsBonsEspetos.Controllers;

// =================================================================
// Definição da Classe (ÚNICA E COMPLETA)
// =================================================================
public class CarrinhoController : Controller
{
    private readonly AppDbContext _context;

    public CarrinhoController(AppDbContext context)
    {
        _context = context;
    }

    // --- Actions (Métodos que respondem a URLs) ---

    // GET: /Carrinho
    public IActionResult Index()
    {
        var carrinho = GetCarrinhoDaSessao();
        return View(carrinho);
    }

    // POST: /Carrinho/Adicionar/5
    [HttpPost]
    public async Task<IActionResult> Adicionar(int id)
    {
        var carrinho = GetCarrinhoDaSessao();
        var itemNoCarrinho = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == id);

        if (itemNoCarrinho != null)
        {
            itemNoCarrinho.Quantidade++;
        }
        else
        {
            var produtoParaAdicionar = await _context.Produtos.FindAsync(id);
            if (produtoParaAdicionar != null)
            {
                carrinho.Itens.Add(new ItemCarrinho
                {
                    ProdutoId = produtoParaAdicionar.Id,
                    Produto = produtoParaAdicionar,
                    Quantidade = 1,
                    PrecoUnitario = produtoParaAdicionar.Preco
                });
            }
        }

        SalvarCarrinhoNaSessao(carrinho);
        return Ok(new { sucesso = true, totalItens = carrinho.Itens.Sum(i => i.Quantidade) });
    }

    // =================================================================
    // MÉTODO REMOVER CORRIGIDO E NO LUGAR CERTO
    // =================================================================
    // POST: /Carrinho/Remover/5
    [HttpPost]
    public IActionResult Remover(int id)
    {
        var carrinho = GetCarrinhoDaSessao();
        var itemParaRemover = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == id);

        if (itemParaRemover != null)
        {
            if (itemParaRemover.Quantidade > 1)
            {
                // Se houver mais de um, apenas diminui a quantidade
                itemParaRemover.Quantidade--;
            }
            else
            {
                // Se houver apenas um, remove o item da lista
                carrinho.Itens.Remove(itemParaRemover);
            }
            
            SalvarCarrinhoNaSessao(carrinho);
        }

        // Redireciona o usuário de volta para a página do carrinho
        return RedirectToAction(nameof(Index));
    }

    // --- Métodos Auxiliares (Privados) ---

    private CarrinhoViewModel GetCarrinhoDaSessao()
    {
        CarrinhoViewModel carrinho;
        string carrinhoJson = HttpContext.Session.GetString("Carrinho");

        if (string.IsNullOrEmpty(carrinhoJson))
        {
            carrinho = new CarrinhoViewModel();
        }
        else
        {
            carrinho = JsonSerializer.Deserialize<CarrinhoViewModel>(carrinhoJson);
        }
        return carrinho;
    }

    private void SalvarCarrinhoNaSessao(CarrinhoViewModel carrinho)
    {
        string carrinhoJson = JsonSerializer.Serialize(carrinho);
        HttpContext.Session.SetString("Carrinho", carrinhoJson);
    }
} // <-- FIM DA CLASSE CARRINHOCONTROLLER
