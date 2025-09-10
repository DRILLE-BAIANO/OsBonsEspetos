using System.ComponentModel.DataAnnotations;
namespace OsBonsEspetosDotNet.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }
        [Range(0.01, 1000.00)]
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
    }
}