using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpireApi.Migrations
{
    /// <inheritdoc />
    public partial class editCurrencyCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Products",
                newName: "CurrencyCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrencyCode",
                table: "Products",
                newName: "Currency");
        }
    }
}
