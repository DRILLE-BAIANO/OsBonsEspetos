// 1. PRIMEIRO: Todas as diretivas 'using'
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OsBonsEspetos.Models;

// 2. DEPOIS: A declaração do namespace
namespace OsBonsEspetos.Data;

// 3. FINALMENTE: A declaração da classe dentro do namespace
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<ItemCarrinho> ItensCarrinho { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Cardapio> Cardapio { get; set; }
}

