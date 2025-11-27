using System.ComponentModel.DataAnnotations.Schema; // Necessário para [NotMapped] e [Column]

namespace OsBonsEspetos.Models;

public class ItemCarrinho
{
    public int Id { get; set; }

    // Relacionamento com o Produto
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    public int Quantidade { get; set; }

    // --- PROPRIEDADES ADICIONADAS ---

    // 1. Preço Unitário
    // Usamos [Column(TypeName = "decimal(18,2)")] para garantir que o banco de dados
    // crie a coluna com a precisão correta para valores monetários.
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoUnitario { get; set; }

    // 2. Subtotal
    // Esta é uma propriedade "calculada". Ela não existe no banco de dados.
    // O valor dela é sempre calculado em tempo real (Quantidade * PrecoUnitario).
    // [NotMapped] informa ao Entity Framework para NÃO criar uma coluna para esta propriedade.
    [NotMapped]
    public decimal Subtotal => Quantidade * PrecoUnitario;

    // Relacionamento com o Carrinho (se você tiver um modelo Carrinho.cs)
    public int CarrinhoId { get; set; }
    public Carrinho? Carrinho { get; set; }
}
