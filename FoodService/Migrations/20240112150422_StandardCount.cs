using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.Migrations
{
    /// <inheritdoc />
    public partial class StandardCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStandard",
                table: "IngredientSalesItem");

            migrationBuilder.AddColumn<int>(
                name: "StandardCount",
                table: "IngredientSalesItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardCount",
                table: "IngredientSalesItem");

            migrationBuilder.AddColumn<bool>(
                name: "IsStandard",
                table: "IngredientSalesItem",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
