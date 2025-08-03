using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyReport.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableDropColumRoleCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ms_Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "ms_Role",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
