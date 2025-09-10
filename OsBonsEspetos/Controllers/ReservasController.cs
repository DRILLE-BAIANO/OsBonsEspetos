using Microsoft.AspNetCore.Mvc;
using OsBonsEspetosDotNet.Data;
using OsBonsEspetosDotNet.Models;
namespace OsBonsEspetosDotNet.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /Reservas/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: /Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NomeCliente,DataReserva,NumeroPessoas")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                _context.SaveChanges();
                return RedirectToAction("Confirmation"); // Exemplo de redirecionamento para uma página de confirmação
            }
            return View(reserva);
        }
        public IActionResult Confirmation()
        {
            return View();
        }
        // POST: /api/reservas (para API RESTful)
        [HttpPost("api/reservas")]
        public IActionResult PostReservaApi([FromBody] Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostReservaApi), new { id = reserva.Id }, reserva);
        }
    }
}