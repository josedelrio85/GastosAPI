using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class Gastos7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FijosMes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    idEntidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FijosMes", x => x.Id);
                    table.UniqueConstraint("IX_FechaIdentidad", x => new { x.fecha, x.idEntidad });
                    table.ForeignKey(
                        name: "FK_FijosMes_Entidad",
                        column: x => x.idEntidad,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FijosMes_idEntidad",
                table: "FijosMes",
                column: "idEntidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FijosMes");
        }
    }
}
