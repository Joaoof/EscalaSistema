using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscalaSistema.API.Migrations
{
    /// <inheritdoc />
    public partial class RefactorScale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Scales",
                newName: "PublishedAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Scales",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Scales");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "Scales",
                newName: "CreatedAt");
        }
    }
}
