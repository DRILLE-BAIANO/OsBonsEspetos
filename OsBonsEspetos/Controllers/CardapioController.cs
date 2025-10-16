using Microsoft.AspNetCore.Mvc;
using OsBonsEspetosDotNet.Data;
using OsBonsEspetosDotNet.Models;
using System.Linq;

// Correção 1: Adicionada a chave de abertura para o namespace
namespace OsBonsEspetosDotNet.Controllers
{ 
    public class CardapioController : Controller
    {
        private readonly AppDbContext _context;

        public CardapioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Cardapio/Index (equivalente ao list do Node.js)
        public IActionResult Index()
        {
            var cardapioItems = _context.Cardapio.ToList();
            return View(cardapioItems); // Passa a lista de itens para a View
        }

        // GET: /Cardapio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Cardapio/Create (equivalente ao create do Node.js)
        [HttpPost]
        [ValidateAntiForgeryToken] // Proteção contra ataques CSRF
        public IActionResult Create([Bind("Nome,Preco,Descricao")] Cardapio cardapio)
        {
            if (ModelState.IsValid) // Verifica as validações do modelo
            {
                _context.Add(cardapio);
                _context.SaveChanges(); // Salva no banco de dados
                return RedirectToAction(nameof(Index)); // Redireciona para a lista
            }
            return View(cardapio); // Retorna a View com erros de validação
        }

        // Correção 2: Adicionado um método GET para a API.
        // O seu método POST "CreatedAtAction" precisa de um método GET para referenciar.
        // Este método busca um item do cardápio pelo ID.
        [HttpGet("api/cardapio/{id}")]
        public IActionResult GetCardapioApi(int id)
        {
            var cardapioItem = _context.Cardapio.Find(id);
            if (cardapioItem == null)
            {
                return NotFound(); // Retorna 404 se não encontrar
            }
            return Ok(cardapioItem); // Retorna o item encontrado com status 200 OK
        }

        // POST: /api/cardapio (para API RESTful)
        [HttpPost("api/cardapio")]
        public IActionResult PostCardapioApi([FromBody] Cardapio cardapio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna erros de validação
            }
            _context.Cardapio.Add(cardapio);
            _context.SaveChanges();
            
            // A linha abaixo cria uma URL para o novo recurso criado (padrão REST)
            // e precisa do método GetCardapioApi que adicionei acima.
            return CreatedAtAction(nameof(GetCardapioApi), new { id = cardapio.Id }, cardapio); // Retorna 201 Created
        }
    } 
    // Correção 3: Adicionada a chave de fechamento para o namespace
}
