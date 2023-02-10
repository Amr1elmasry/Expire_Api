using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpireApi.Migrations
{
    /// <inheritdoc />
    public partial class makedefaultCurrencyCodeto818 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "EGP",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrencyCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "EGP");
        }
    }
}
