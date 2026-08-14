using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriConect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConversationDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ClientDeletedAt",
                table: "Conversations",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NutritionistDeletedAt",
                table: "Conversations",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientDeletedAt",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "NutritionistDeletedAt",
                table: "Conversations");
        }
    }
}
