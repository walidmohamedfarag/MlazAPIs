using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlazAPIs.Migrations
{
    /// <inheritdoc />
    public partial class addimagepublicidtoreporttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Reports");
        }
    }
}
