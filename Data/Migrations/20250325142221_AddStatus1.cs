using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easymed_mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatus1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Doctor",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Doctor",
                newName: "Active");
        }
    }
}
