using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class Gastos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombreEntidad = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposMovimiento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tipoMovimiento = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMovimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activo = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    EntidadId = table.Column<int>(nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Fijo = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Importe = table.Column<float>(type: "float", nullable: false, defaultValue: 0f),
                    TipoId = table.Column<int>(nullable: true),
                    idEmisor = table.Column<int>(nullable: false),
                    idTipoMovimiento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gastos_Entidades_EntidadId",
                        column: x => x.EntidadId,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gastos_TiposMovimiento_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_EntidadId",
                table: "Gastos",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_TipoId",
                table: "Gastos",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_EmisorGasto",
                table: "Gastos",
                column: "idEmisor");

            migrationBuilder.CreateIndex(
                name: "IX_FK_TipoMovimientoGasto",
                table: "Gastos",
                column: "idTipoMovimiento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "Entidades");

            migrationBuilder.DropTable(
                name: "TiposMovimiento");
        }
    }
}
