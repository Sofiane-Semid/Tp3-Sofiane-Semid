using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flappyBirb_serveur.Migrations
{
    /// <inheritdoc />
    public partial class flappy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "Score");

            migrationBuilder.AddColumn<string>(
                name: "PseudoId",
                table: "Score",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Score_PseudoId",
                table: "Score",
                column: "PseudoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_AspNetUsers_PseudoId",
                table: "Score",
                column: "PseudoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_AspNetUsers_PseudoId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_PseudoId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "PseudoId",
                table: "Score");

            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "Score",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
