#nullable enable

// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace OsBonsEspetos.Models;

// Herde de IdentityUser para adicionar novos campos
public class ApplicationUser : IdentityUser
{
    // Adicione as propriedades que você deseja
    // Por exemplo, um campo para o nome e outro para a foto
    public string NomeCompleto { get; set; }
    public string Foto { get; set; } // O campo que está faltando!
}
