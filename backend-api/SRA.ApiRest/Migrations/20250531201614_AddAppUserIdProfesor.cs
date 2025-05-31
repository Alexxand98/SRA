using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRA.ApiRest.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserIdProfesor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Profesores",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_AppUserId",
                table: "Profesores",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesores_AspNetUsers_AppUserId",
                table: "Profesores",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesores_AspNetUsers_AppUserId",
                table: "Profesores");

            migrationBuilder.DropIndex(
                name: "IX_Profesores_AppUserId",
                table: "Profesores");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Profesores");
        }
    }
}
