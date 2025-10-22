using System;
using System.ComponentModel.DataAnnotations;
namespace OsBonsEspetos.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100)]
        public string NomeCliente { get; set; } // Propriedade renomeada para seguir convenção C#
        [Required(ErrorMessage = "A data da reserva é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataReserva { get; set; }
        [Range(1, 20, ErrorMessage = "O número de pessoas deve estar entre 1 e 20.")]
        public int NumeroPessoas { get; set; }
    }
}