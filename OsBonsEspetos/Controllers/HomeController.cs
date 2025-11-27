// =================================================================
// Usings (Importações) - Adicionamos as que faltavam
// =================================================================
using System.Diagnostics;
using System.Threading.Tasks; // Necessário para programação assíncrona
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para usar ToListAsync()
using OsBonsEspetos.Data;       // Necessário para usar o AppDbContext
using OsBonsEspetos.Models;
using OsBonsEspetos.ViewModels; 

namespace OsBonsEspetos.Controllers;

public class HomeController : Controller
{
    // =================================================================
    // Injeção de Dependência
    // =================================================================
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context; // Variável para guardar o acesso ao banco

    // O construtor agora recebe o AppDbContext além do ILogger
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context; // O sistema entrega o acesso ao banco, e nós o guardamos
    }

    // =================================================================
    // Action 'Index' - Corrigida
    // =================================================================
    public async Task<IActionResult> Index()
    {
        // 1. Busca a lista de produtos no banco de dados de forma assíncrona
        var produtos = await _context.Produtos.ToListAsync();
        
        // 2. Envia a lista de produtos para a View. 
        //    Agora, a variável 'Model' na sua View não será mais nula.
        return View(produtos);
    }

    // =================================================================
    // Outras Actions (não precisam de alteração)
    // =================================================================
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
