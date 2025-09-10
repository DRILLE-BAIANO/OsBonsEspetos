using Microsoft.AspNetCore.Mvc;
using OsBonsEspetosDotNet.Data; 
using OsBonsEspetosDotNet.Models;
using System.Linq; 
namespace OsBonsEspetosDotNet.Controllers

    public class CardapioController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CardapioController(ApplicationDbContext context)
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
            return CreatedAtAction(nameof(GetCardapioApi), new { id = cardapio.Id }, cardapio); // Retorna 201 Created
        }
    }
