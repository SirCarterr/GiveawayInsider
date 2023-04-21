using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiveAwayInsider_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Notification3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Notifications");
        }
    }
}
