using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3214EC2718DCCB36", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IngredientPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ingredie__3214EC2778E4B991", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SalesItemGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SalesItemGroup__3214EC27745E8750", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Location = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "int", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shop__3214EC2718F16219", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ProductGroupID = table.Column<int>(type: "int", nullable: true),
                    CustomerGroupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discount__3214EC272E754D2D", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Discount__Custom__49C3F6B7",
                        column: x => x.CustomerGroupID,
                        principalTable: "CustomerGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Discount__SalesIte__48CFD27E",
                        column: x => x.ProductGroupID,
                        principalTable: "SalesItemGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(30)", unicode: false, maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsComposite = table.Column<bool>(type: "bit", nullable: false),
                    SalesItemGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SalesItem__3214EC2714F20DB5", x => x.ID);
                    table.ForeignKey(
                        name: "FK__SalesItem__SalesItem__286302EC",
                        column: x => x.SalesItemGroupID,
                        principalTable: "SalesItemGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    Datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ShopID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderData__3214EC27...", x => x.ID);
                    table.ForeignKey(
                        name: "FK__OrderData__ShopID__32E0915F",
                        column: x => x.ShopID,
                        principalTable: "Shop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientSalesItem",
                columns: table => new
                {
                    SalesItemID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Min = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ingredie__0FE62DCA5B3496C2", x => new { x.SalesItemID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK__Ingredien__Ingre__300424B4",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Ingredien__SalesIte__2F10007B",
                        column: x => x.SalesItemID,
                        principalTable: "SalesItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesItemCompositions",
                columns: table => new
                {
                    ParentItemId = table.Column<int>(type: "int", nullable: false),
                    ChildItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesItemCompositions", x => new { x.ParentItemId, x.ChildItemId });
                    table.ForeignKey(
                        name: "FK_SalesItemCompositions_SalesItem_ChildItemId",
                        column: x => x.ChildItemId,
                        principalTable: "SalesItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesItemCompositions_SalesItem_ParentItemId",
                        column: x => x.ParentItemId,
                        principalTable: "SalesItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopSalesItem",
                columns: table => new
                {
                    SalesItemId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopSalesItem", x => new { x.SalesItemId, x.ShopId });
                    table.ForeignKey(
                        name: "FK__ShopSalesItem__SalesItemId",
                        column: x => x.SalesItemId,
                        principalTable: "SalesItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ShopSalesItem__ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orderline",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderlinePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    SalesItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orderlin__3214EC273B1DF0EA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orderline_SalesItem_SalesItemId",
                        column: x => x.SalesItemId,
                        principalTable: "SalesItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Orderline__Order__36B12243",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientOrderline",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    OrderlineID = table.Column<int>(type: "int", nullable: false),
                    Delta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ingredie__8127A0A74100BD35", x => new { x.IngredientID, x.OrderlineID });
                    table.ForeignKey(
                        name: "FK__Ingredien__Ingre__4316F928",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Ingredien__Order__440B1D61",
                        column: x => x.OrderlineID,
                        principalTable: "Orderline",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_CustomerGroupID",
                table: "Discount",
                column: "CustomerGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ProductGroupID",
                table: "Discount",
                column: "ProductGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Name",
                table: "Ingredient",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientOrderline_OrderlineID",
                table: "IngredientOrderline",
                column: "OrderlineID");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientSalesItem_IngredientID",
                table: "IngredientSalesItem",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShopID",
                table: "Order",
                column: "ShopID");

            migrationBuilder.CreateIndex(
                name: "IX_Orderline_OrderId",
                table: "Orderline",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orderline_SalesItemId",
                table: "Orderline",
                column: "SalesItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItem_SalesItemGroupID",
                table: "SalesItem",
                column: "SalesItemGroupID");

            migrationBuilder.CreateIndex(
                name: "UQ__SalesItem__49A3C8398A3F3285",
                table: "SalesItem",
                column: "ProductNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesItemCompositions_ChildItemId",
                table: "SalesItemCompositions",
                column: "ChildItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopSalesItem_ShopId",
                table: "ShopSalesItem",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "IngredientOrderline");

            migrationBuilder.DropTable(
                name: "IngredientSalesItem");

            migrationBuilder.DropTable(
                name: "SalesItemCompositions");

            migrationBuilder.DropTable(
                name: "ShopSalesItem");

            migrationBuilder.DropTable(
                name: "CustomerGroup");

            migrationBuilder.DropTable(
                name: "Orderline");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "SalesItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SalesItemGroup");

            migrationBuilder.DropTable(
                name: "Shop");
        }
    }
}
