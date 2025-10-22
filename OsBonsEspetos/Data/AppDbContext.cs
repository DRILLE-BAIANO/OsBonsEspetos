using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OsBonsEspetos.Models; // Importa os modelos

namespace OsBonsEspetos.Data
{
    // CORREÇÃO: A classe deve herdar de IdentityDbContext<Usuario>
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Seus DbSets (tabelas)
        public DbSet<Cardapio> Cardapio { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        // Não é necessário DbSet<Usuario> aqui, pois IdentityDbContext já o fornece.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CORREÇÃO ESSENCIAL: Chamar o base.OnModelCreating para configurar o Identity
            base.OnModelCreating(modelBuilder);

            // CORREÇÃO: Usando 'modelBuilder' em vez de 'builder'

            // Mapeamento de tabelas do Identity para nomes personalizados (se necessário)
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("usuario_perfil");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("usuario_login");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("usuario_regra");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("usuario_token");
            modelBuilder.Entity<IdentityRole>().ToTable("perfil");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("perfil_regra");
            
            // Mapeamento das suas entidades
            modelBuilder.Entity<Cardapio>().ToTable("cardapio");
            modelBuilder.Entity<Reserva>().Property(r => r.NomeCliente).HasColumnName("nome_cliente");
        }
    }
}
