using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiveAwayInsider_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Notifications2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Timer",
                table: "Notifications",
                newName: "Search");

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sort",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Search",
                table: "Notifications",
                newName: "Timer");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
