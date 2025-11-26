using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsBonsEspetos.Data;
using OsBonsEspetos.Models;
using System.Threading.Tasks;

namespace OsBonsEspetos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarrinhoController : ControllerBase
{
    private readonly AppDbContext _context;

    public CarrinhoController(AppDbContext context)
    {
        _context = context;
    }

    // POST /api/carrinho/adicionar/{produtoId}
    [HttpPost("adicionar/{produtoId}")]
    public async Task<IActionResult> AdicionarItem(int produtoId)
    {
        var produto = await _context.Produtos.FindAsync(produtoId);
        if (produto == null)
        {
            return NotFound(new { success = false, message = "Produto não encontrado." });
        }

        // Tenta obter o ID do carrinho da sessão do usuário
        int? carrinhoId = HttpContext.Session.GetInt32("CarrinhoId");
        Carrinho carrinho;

        if (carrinhoId.HasValue)
        {
            // Se encontrou um ID na sessão, busca o carrinho no banco
            carrinho = await _context.Carrinhos
                                     .Include(c => c.Itens)
                                     .FirstOrDefaultAsync(c => c.Id == carrinhoId.Value);
        }
        else
        {
            // Se não há carrinho na sessão, cria um novo
            carrinho = new Carrinho();
            _context.Carrinhos.Add(carrinho);
            await _context.SaveChangesAsync(); // Salva para gerar o ID
            HttpContext.Session.SetInt32("CarrinhoId", carrinho.Id); // Salva o novo ID na sessão
        }

        // Procura pelo item específico no carrinho
        var itemCarrinho = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);

        if (itemCarrinho != null)
        {
            // Se o produto já existe no carrinho, incrementa a quantidade
            itemCarrinho.Quantidade++;
        }
        else
        {
            // Se não, adiciona um novo item
            itemCarrinho = new ItemCarrinho
            {
                ProdutoId = produtoId,
                Quantidade = 1
            };
            carrinho.Itens.Add(itemCarrinho);
        }

        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "Produto adicionado com sucesso!" });
    }
}

