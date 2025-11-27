// =================================================================
// Usings (Importações)
// =================================================================
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para ToListAsync()
using OsBonsEspetos.Data;
using OsBonsEspetos.Models;
using System.Threading.Tasks; // Necessário para Task<IActionResult>

namespace OsBonsEspetos.Controllers; // Namespace simplificado

// =================================================================
// Definição da Classe (ÚNICA E COMPLETA)
// =================================================================
public class CardapioController : Controller
{
    // =================================================================
    // Injeção de Dependência
    // =================================================================
    private readonly AppDbContext _context;

    public CardapioController(AppDbContext context)
    {
        _context = context; // O sistema entrega o acesso ao banco de dados
    }

    // =================================================================
    // Actions (Métodos que respondem a URLs)
    // =================================================================

    // GET: /Cardapio ou /Cardapio/Index
    // Este método exibe a lista de todos os produtos (o cardápio).
    public async Task<IActionResult> Index()
    {
        // Busca os produtos no banco de dados, incluindo a informação da Categoria
        var produtos = await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        
        // Envia a lista de produtos para a View correspondente
        return View(produtos);
    }

    // GET: /Cardapio/Detalhes/5
    // Mostra os detalhes de um único produto.
    public async Task<IActionResult> Detalhes(int? id)
    {
        if (id == null)
        {
            return NotFound(); // Retorna erro 404 se nenhum ID for fornecido
        }

        var produto = await _context.Produtos
            .Include(p => p.Categoria) // Inclui a categoria do produto
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (produto == null)
        {
            return NotFound(); // Retorna erro 404 se o produto não for encontrado
        }

        return View(produto); // Envia o produto encontrado para a View "Detalhes"
    }
    
    // NOTA: Removi os métodos Create e os de API para simplificar e focar na funcionalidade principal.
    // Podemos adicioná-los de volta depois, se você precisar de uma área para gerenciar produtos.
}
