using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAplicativoEnsaio.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnsaioMusicRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnsaioId",
                table: "Musics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musics_EnsaioId",
                table: "Musics",
                column: "EnsaioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Rehearsals_EnsaioId",
                table: "Musics",
                column: "EnsaioId",
                principalTable: "Rehearsals",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Rehearsals_EnsaioId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_EnsaioId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "EnsaioId",
                table: "Musics");
        }
    }
}
