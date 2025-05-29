using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRA.ApiRest.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityDtoExtendido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FranjaHoraria",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Profesor",
                table: "Reservas");

            migrationBuilder.AlterColumn<string>(
                name: "Grupo",
                table: "Reservas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Reservas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "FranjaHorariaId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfesorId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DiasNoLectivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiasNoLectivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FranjasHorarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraInicio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoraFin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranjasHorarias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UltimoAcceso = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_FranjaHorariaId",
                table: "Reservas",
                column: "FranjaHorariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ProfesorId",
                table: "Reservas",
                column: "ProfesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_FranjasHorarias_FranjaHorariaId",
                table: "Reservas",
                column: "FranjaHorariaId",
                principalTable: "FranjasHorarias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Profesores_ProfesorId",
                table: "Reservas",
                column: "ProfesorId",
                principalTable: "Profesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_FranjasHorarias_FranjaHorariaId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Profesores_ProfesorId",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "DiasNoLectivos");

            migrationBuilder.DropTable(
                name: "FranjasHorarias");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_FranjaHorariaId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_ProfesorId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "FranjaHorariaId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "ProfesorId",
                table: "Reservas");

            migrationBuilder.AlterColumn<string>(
                name: "Grupo",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "FranjaHoraria",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profesor",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
