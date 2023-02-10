using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpireApi.Migrations
{
    /// <inheritdoc />
    public partial class editrelationbetweenproductandmarketto1tomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketProduct");

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MarketId",
                table: "Products",
                column: "MarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Markets_MarketId",
                table: "Products",
                column: "MarketId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Markets_MarketId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MarketId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "MarketProduct",
                columns: table => new
                {
                    MarketsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketProduct", x => new { x.MarketsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_MarketProduct_Markets_MarketsId",
                        column: x => x.MarketsId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketProduct_ProductsId",
                table: "MarketProduct",
                column: "ProductsId");
        }
    }
}
