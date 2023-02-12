using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpireApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDayesToReminderBeforExpiretotablesproductcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayesToReminderBeforExpire",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayesToReminderBeforExpire",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayesToReminderBeforExpire",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DayesToReminderBeforExpire",
                table: "Categories");
        }
    }
}
