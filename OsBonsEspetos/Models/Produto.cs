// Os usings podem ser removidos se não forem usados, mas não fazem mal.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsBonsEspetos.Models; // Apenas uma declaração de namespace

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }
    public string? ImagemUrl { get; set; }

    // As propriedades para o relacionamento estão corretas!
    public int CategoriaId { get; set; } // Chave estrangeira
    public Categoria? Categoria { get; set; } // Propriedade de navegação
}
