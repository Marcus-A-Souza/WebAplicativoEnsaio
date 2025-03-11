using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAplicativoEnsaio.Migrations
{
    /// <inheritdoc />
    public partial class AddEnsaiosDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Rehearsals_EnsaioId",
                table: "Musics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rehearsals",
                table: "Rehearsals");

            migrationBuilder.RenameTable(
                name: "Rehearsals",
                newName: "Ensaios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ensaios",
                table: "Ensaios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Ensaios_EnsaioId",
                table: "Musics",
                column: "EnsaioId",
                principalTable: "Ensaios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Ensaios_EnsaioId",
                table: "Musics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ensaios",
                table: "Ensaios");

            migrationBuilder.RenameTable(
                name: "Ensaios",
                newName: "Rehearsals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rehearsals",
                table: "Rehearsals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Rehearsals_EnsaioId",
                table: "Musics",
                column: "EnsaioId",
                principalTable: "Rehearsals",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
