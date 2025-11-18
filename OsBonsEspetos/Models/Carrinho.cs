using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace OsBonsEspetos.Models
{
  public class Carrinho
{
    public int Id { get; set; }
    public int? UsuarioId { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
}

}