using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlazAPIs.Migrations
{
    /// <inheritdoc />
    public partial class adddescriptionprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reports");
        }
    }
}
