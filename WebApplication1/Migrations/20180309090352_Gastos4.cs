using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class Gastos4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Entidades_EntidadId",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_TiposMovimiento_TipoId",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_EntidadId",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_TipoId",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "EntidadId",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Gastos");

            migrationBuilder.RenameIndex(
                name: "IX_FK_TipoMovimientoGasto",
                table: "Gastos",
                newName: "IX_Gastos_idTipoMovimiento");

            migrationBuilder.RenameIndex(
                name: "IX_FK_EmisorGasto",
                table: "Gastos",
                newName: "IX_Gastos_idEntidad");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Entidades_idEntidad",
                table: "Gastos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_TiposMovimiento_idTipoMovimiento",
                table: "Gastos");

            migrationBuilder.RenameIndex(
                name: "IX_Gastos_idTipoMovimiento",
                table: "Gastos",
                newName: "IX_FK_TipoMovimientoGasto");

            migrationBuilder.RenameIndex(
                name: "IX_Gastos_idEntidad",
                table: "Gastos",
                newName: "IX_FK_EmisorGasto");

            migrationBuilder.AddColumn<int>(
                name: "EntidadId",
                table: "Gastos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "Gastos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_EntidadId",
                table: "Gastos",
                column: "EntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_TipoId",
                table: "Gastos",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Entidades_EntidadId",
                table: "Gastos",
                column: "EntidadId",
                principalTable: "Entidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_TiposMovimiento_TipoId",
                table: "Gastos",
                column: "TipoId",
                principalTable: "TiposMovimiento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
