// OsBonsEspetos/Program.cs
using OsBonsEspetos.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OsBonsEspetos.Data; // Assumindo que você terá uma pasta Data para o DbContext
using Microsoft.EntityFrameworkCore;

// Adicionar serviços ao contêiner.
var builder = WebApplication.CreateBuilder(args);

// 1. Pegar a string de conexão do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Adicionar o DbContext ao contêiner de serviços
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

// ... adicione outros serviços aqui (controllers, etc.)

var app = builder.Build();

// Configurar o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Habilita o serviço de arquivos estáticos (wwwroot)

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
