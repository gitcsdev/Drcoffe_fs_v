using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrCoffee_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomizationOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomizationOptions",
                columns: table => new
                {
                    CustomizationOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomizationOptions", x => x.CustomizationOptionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomizationOptions_OptionCode",
                table: "CustomizationOptions",
                column: "OptionCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomizationOptions");
        }
    }
}
