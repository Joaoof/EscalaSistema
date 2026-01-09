using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscalaSistema.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MusicId",
                table: "Cults",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Scales_CultId",
                table: "Scales",
                column: "CultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScaleAssignments_MusicianId",
                table: "ScaleAssignments",
                column: "MusicianId");

            migrationBuilder.CreateIndex(
                name: "IX_ScaleAssignments_ScaleId",
                table: "ScaleAssignments",
                column: "ScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Musics_CultId",
                table: "Musics",
                column: "CultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Cults_CultId",
                table: "Musics",
                column: "CultId",
                principalTable: "Cults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScaleAssignments_Musicians_MusicianId",
                table: "ScaleAssignments",
                column: "MusicianId",
                principalTable: "Musicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScaleAssignments_Scales_ScaleId",
                table: "ScaleAssignments",
                column: "ScaleId",
                principalTable: "Scales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scales_Cults_CultId",
                table: "Scales",
                column: "CultId",
                principalTable: "Cults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Cults_CultId",
                table: "Musics");

            migrationBuilder.DropForeignKey(
                name: "FK_ScaleAssignments_Musicians_MusicianId",
                table: "ScaleAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ScaleAssignments_Scales_ScaleId",
                table: "ScaleAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Scales_Cults_CultId",
                table: "Scales");

            migrationBuilder.DropIndex(
                name: "IX_Scales_CultId",
                table: "Scales");

            migrationBuilder.DropIndex(
                name: "IX_ScaleAssignments_MusicianId",
                table: "ScaleAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ScaleAssignments_ScaleId",
                table: "ScaleAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Musics_CultId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "MusicId",
                table: "Cults");
        }
    }
}
