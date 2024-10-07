using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriConect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTabelaProfessionalEvaluations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalEvaluations_Clients_ClientId",
                table: "ProfessionalEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_ProfessionalEvaluations_ClientId",
                table: "ProfessionalEvaluations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalEvaluations_ClientId",
                table: "ProfessionalEvaluations",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalEvaluations_Clients_ClientId",
                table: "ProfessionalEvaluations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
