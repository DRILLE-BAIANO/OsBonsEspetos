using OsBonsEspetos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsBonsEspetos.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OsBonsEspetos.Controllers;

// Este controller agora vai gerenciar tanto a API quanto as Views do carrinho
public class CarrinhoController : Controller
{
    private readonly AppDbContext _context;

    public CarrinhoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Carrinho
    // Este método vai exibir a página do carrinho de compras
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Lógica para obter o carrinho do usuário
        // Por enquanto, vamos pegar o primeiro carrinho do banco como exemplo
        // (Depois podemos melhorar isso com sessões ou login)
        var carrinho = await _context.Carrinhos
            .Include(c => c.Itens) // Inclui os ItensCarrinho relacionados
            .ThenInclude(i => i.Produto) // Para cada item, inclui os dados do Produto
            .FirstOrDefaultAsync();

        if (carrinho == null)
        {
            // Se não houver carrinho, mostra a página com uma lista vazia
            return View(new List<ItemCarrinho>());
        }

        return View(carrinho.Itens);
    }

    // ... (aqui fica o método da API que já criamos antes)
    // POST /api/carrinho/adicionar/{produtoId}
    [HttpPost("api/adicionar/{produtoId}")]
    public IActionResult AdicionarItem(int produtoId)
    {
        // ... código para adicionar item que já fizemos
     
     return RedirectToAction("Index");
   
    }

}
