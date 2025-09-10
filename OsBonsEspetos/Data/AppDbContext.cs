using Microsoft.EntityFrameworkCore;
using OsBonsEspetosDotNet.Models; // Importa os modelos
namespace OsBonsEspetosDotNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Representa as tabelas no banco de dados
        public DbSet<Cardapio> Cardapio { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Produto> Produtos { get; set; } // Adicionar DbSet para Produtos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cardapio>().ToTable("cardapio");
            modelBuilder.Entity<Reserva>().Property(r => r.NomeCliente).HasColumnName("nome_cliente");
        }
    }
}