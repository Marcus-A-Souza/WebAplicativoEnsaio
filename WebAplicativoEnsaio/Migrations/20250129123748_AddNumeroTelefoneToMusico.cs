using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAplicativoEnsaio.Migrations
{
    /// <inheritdoc />
    public partial class AddNumeroTelefoneToMusico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroTelefone",
                table: "Musicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroTelefone",
                table: "Musicos");
        }
    }
}
