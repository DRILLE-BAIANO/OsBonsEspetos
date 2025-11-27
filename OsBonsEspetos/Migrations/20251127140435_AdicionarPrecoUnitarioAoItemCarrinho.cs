using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsBonsEspetos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarPrecoUnitarioAoItemCarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitario",
                table: "ItensCarrinho",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoUnitario",
                table: "ItensCarrinho");
        }
    }
}
