using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS_DALL.Migrations
{
    /// <inheritdoc />
    public partial class _maxvalue___ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CH_Limit_Max",
                table: "Users",
                sql: "[Limit]>=0 AND [Limit]<=10000");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CH_Limit_Max",
                table: "Users");
        }
    }
}
