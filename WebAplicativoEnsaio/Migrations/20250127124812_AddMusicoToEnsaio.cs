using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAplicativoEnsaio.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicoToEnsaio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnsaioId",
                table: "Musicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Musicos_EnsaioId",
                table: "Musicos",
                column: "EnsaioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicos_Ensaios_EnsaioId",
                table: "Musicos",
                column: "EnsaioId",
                principalTable: "Ensaios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicos_Ensaios_EnsaioId",
                table: "Musicos");

            migrationBuilder.DropIndex(
                name: "IX_Musicos_EnsaioId",
                table: "Musicos");

            migrationBuilder.DropColumn(
                name: "EnsaioId",
                table: "Musicos");
        }
    }
}
