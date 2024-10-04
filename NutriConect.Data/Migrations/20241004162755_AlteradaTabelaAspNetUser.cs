using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriConect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteradaTabelaAspNetUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "RecipeEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "varchar(256)", nullable: false),
                    Title = table.Column<string>(type: "varchar(256)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ProfessionalId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeEvaluations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeEvaluations_Professionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "Professionals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeEvaluations_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEvaluations_ClientId",
                table: "RecipeEvaluations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEvaluations_ProfessionalId",
                table: "RecipeEvaluations",
                column: "ProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEvaluations_RecipeId",
                table: "RecipeEvaluations",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeEvaluations");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
