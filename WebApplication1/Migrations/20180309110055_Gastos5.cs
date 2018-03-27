using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class Gastos5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Entidades_idEntidad",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_TiposMovimiento_idTipoMovimiento",
                table: "Gastos");

            migrationBuilder.AlterColumn<double>(
                name: "Importe",
                table: "Gastos",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(float),
                oldType: "float",
                oldDefaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Entidad",
                table: "Gastos",
                column: "idEntidad",
                principalTable: "Entidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Tipo",
                table: "Gastos",
                column: "idTipoMovimiento",
                principalTable: "TiposMovimiento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Entidad",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Tipo",
                table: "Gastos");

            migrationBuilder.AlterColumn<float>(
                name: "Importe",
                table: "Gastos",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Entidades_idEntidad",
                table: "Gastos",
                column: "idEntidad",
                principalTable: "Entidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_TiposMovimiento_idTipoMovimiento",
                table: "Gastos",
                column: "idTipoMovimiento",
                principalTable: "TiposMovimiento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
