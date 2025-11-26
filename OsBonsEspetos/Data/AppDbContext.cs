// =================================================================
// Usings (Importações)
// =================================================================
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OsBonsEspetos.Models;

// =================================================================
// Namespace
// =================================================================
namespace OsBonsEspetos.Data;

// =================================================================
// Definição da Classe (ÚNICA E COMPLETA)
// =================================================================
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // --- DbSets: Mapeamento das suas classes para tabelas do banco ---

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; } // A NOVA LINHA, NO LUGAR CERTO
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<ItemCarrinho> ItensCarrinho { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    
    // Nota: Removi o DbSet<Cardapio> pois parece que você não tem um modelo para ele.
    // Se você tiver um modelo Cardapio.cs, pode adicionar a linha de volta.
    // public DbSet<Cardapio> Cardapio { get; set; } 
}
