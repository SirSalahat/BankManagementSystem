using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS_DALL.Migrations
{
    /// <inheritdoc />
    public partial class limit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Limit",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Users");
        }
    }
}
