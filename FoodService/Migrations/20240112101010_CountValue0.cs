using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.Migrations
{
    /// <inheritdoc />
    public partial class CountValue0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "IngredientSalesItem",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((1))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "IngredientSalesItem",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((0))");
        }
    }
}
