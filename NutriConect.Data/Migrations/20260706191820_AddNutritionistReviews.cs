using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriConect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNutritionistReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionistReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NutritionistId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionistReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionistReviews_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NutritionistReviews_AspNetUsers_NutritionistId",
                        column: x => x.NutritionistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionistReviews_AuthorId",
                table: "NutritionistReviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionistReviews_NutritionistId_AuthorId",
                table: "NutritionistReviews",
                columns: new[] { "NutritionistId", "AuthorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionistReviews");
        }
    }
}
