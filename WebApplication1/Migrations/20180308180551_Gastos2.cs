using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class Gastos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FK_EmisorGasto",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_FK_TipoMovimientoGasto",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "idEmisor",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "idTipoMovimiento",
                table: "Gastos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idEmisor",
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
                column: "idEmisor");

            migrationBuilder.CreateIndex(
                name: "IX_FK_TipoMovimientoGasto",
                table: "Gastos",
                column: "idTipoMovimiento");
        }
    }
}
