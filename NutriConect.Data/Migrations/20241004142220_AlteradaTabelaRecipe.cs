using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriConect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteradaTabelaRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionalId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ClientId",
                table: "Recipes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProfessionalId",
                table: "Recipes",
                column: "ProfessionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Clients_ClientId",
                table: "Recipes",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Professionals_ProfessionalId",
                table: "Recipes",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Clients_ClientId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Professionals_ProfessionalId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ClientId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProfessionalId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProfessionalId",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
