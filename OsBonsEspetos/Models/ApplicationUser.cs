// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace OsBonsEspetos.Models;

public class ApplicationUser : IdentityUser
{
    // Adicione '?' para indicar que estas propriedades podem ser nulas.
    public string? NomeCompleto { get; set; }
    public string? Foto { get; set; }
}
