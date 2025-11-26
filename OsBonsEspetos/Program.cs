// =================================================================
// SEÇÃO 1: Usings (Importações)
// =================================================================
using OsBonsEspetos.Data;
using OsBonsEspetos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// =================================================================
// SEÇÃO 2: Configuração do "Builder"
// =================================================================
var builder = WebApplication.CreateBuilder(args);

// --- Adicionar serviços ao contêiner ---

// 1. Configuração do Banco de Dados e Ferramentas de Diagnóstico do EF
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Esta linha habilita as páginas de erro detalhadas para o banco de dados
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2. Configuração do Identity (Sistema de Login)
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

// 3. Configuração da Sessão (para o Carrinho de Compras)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 4. Configuração dos Controllers e Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


// =================================================================
// SEÇÃO 3: Construção do "App"
// =================================================================
var app = builder.Build();


// =================================================================
// SEÇÃO 4: Configuração do Pipeline de Requisições HTTP
// =================================================================

// Configurar o pipeline para o ambiente de desenvolvimento ou produção
if (app.Environment.IsDevelopment())
{
    // Em desenvolvimento, usa o endpoint de migrações.
    // Esta linha agora funcionará porque o pacote foi instalado.
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento final das rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


// =================================================================
// SEÇÃO 5: Execução do App
// =================================================================
app.Run();
