using System.ComponentModel.DataAnnotations; // Para validações
namespace OsBonsEspetos.Models
{
    public class Cardapio
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }
        [Range(0.01, 1000.00, ErrorMessage = "O preço deve estar entre 0.01 e 1000.00.")]
        public decimal Preco { get; set; }
        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Descricao { get; set; }
    }
}