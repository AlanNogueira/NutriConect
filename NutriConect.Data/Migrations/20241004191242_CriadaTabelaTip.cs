using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriConect.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriadaTabelaTip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "RecipeEvaluations",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");

            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "varchar(max)", nullable: false),
                    Title = table.Column<string>(type: "varchar(256)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ProfessionalId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tips_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tips_Professionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "Professionals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TipEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "varchar(max)", nullable: false),
                    Title = table.Column<string>(type: "varchar(256)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    TipId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ProfessionalId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipEvaluations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TipEvaluations_Professionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "Professionals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TipEvaluations_Tips_TipId",
                        column: x => x.TipId,
                        principalTable: "Tips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipEvaluations_ClientId",
                table: "TipEvaluations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TipEvaluations_ProfessionalId",
                table: "TipEvaluations",
                column: "ProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_TipEvaluations_TipId",
                table: "TipEvaluations",
                column: "TipId");

            migrationBuilder.CreateIndex(
                name: "IX_Tips_ClientId",
                table: "Tips",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tips_ProfessionalId",
                table: "Tips",
                column: "ProfessionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipEvaluations");

            migrationBuilder.DropTable(
                name: "Tips");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "RecipeEvaluations",
                type: "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");
        }
    }
}
