using Microsoft.AspNetCore.Mvc;
using OsBonsEspetosDotNet.Data;
using OsBonsEspetosDotNet.Models;
namespace OsBonsEspetosDotNet.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /Produtos/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: /Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nome,Preco,Descricao")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redireciona para a lista de produtos
            }
            return View(produto);
        }
        // GET: /Produtos/Index
        public IActionResult Index()
        {
            var produtos = _context.Produtos.ToList();
            return View(produtos);
        }
        // POST: /api/produtos (para API RESTful)
        [HttpPost("api/produtos")]
        public IActionResult PostProdutoApi([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostProdutoApi), new { id = produto.Id }, produto);
        }
    }
}