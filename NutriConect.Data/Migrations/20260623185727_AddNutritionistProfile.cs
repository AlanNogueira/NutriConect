using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriConect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNutritionistProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptsInPerson",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptsOnline",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AvailableForNewPatients",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethods",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionDurationMinutes",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialties",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NutritionistCredentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NutritionistId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionistCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionistCredentials_AspNetUsers_NutritionistId",
                        column: x => x.NutritionistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionistCredentials_NutritionistId",
                table: "NutritionistCredentials",
                column: "NutritionistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionistCredentials");

            migrationBuilder.DropColumn(
                name: "AcceptsInPerson",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AcceptsOnline",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvailableForNewPatients",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OfficeAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PaymentMethods",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SessionDurationMinutes",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Specialties",
                table: "AspNetUsers");
        }
    }
}
