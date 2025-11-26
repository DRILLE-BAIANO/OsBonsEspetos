using System.Collections.Generic; // Necessário para usar List<T>

namespace OsBonsEspetos.Models;

public class Categoria
{
    public int Id { get; set; }

    // A linha foi corrigida para inicializar com uma string vazia.
    public string Nome { get; set; } = string.Empty;

    // É uma boa prática adicionar a lista de produtos relacionados.
    // Isso ajuda o Entity Framework a entender a relação "um-para-muitos".
    // (Uma Categoria tem muitos Produtos).
    public List<Produto> Produtos { get; set; } = new List<Produto>();
}
