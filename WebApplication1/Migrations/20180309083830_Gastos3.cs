using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class Gastos3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idEntidad",
                table: "Gastos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idTipoMovimiento",
                table: "Gastos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FK_EmisorGasto",
                table: "Gastos",
                column: "idEntidad");

            migrationBuilder.CreateIndex(
                name: "IX_FK_TipoMovimientoGasto",
                table: "Gastos",
                column: "idTipoMovimiento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FK_EmisorGasto",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_FK_TipoMovimientoGasto",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "idEntidad",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "idTipoMovimiento",
                table: "Gastos");
        }
    }
}
