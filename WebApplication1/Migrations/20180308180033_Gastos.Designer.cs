﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApplication1.Context;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(ModeloContext))]
    [Migration("20180308180033_Gastos")]
    partial class Gastos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication1.Modelo.Entidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("nombreEntidad")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Entidades");
                });

            modelBuilder.Entity("WebApplication1.Modelo.Gasto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Activo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Activo")
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("EntidadId");

                    b.Property<DateTime>("Fecha")
                        .HasColumnName("fecha")
                        .HasColumnType("datetime");

                    b.Property<int?>("Fijo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Fijo")
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<float>("Importe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0f);

                    b.Property<int?>("TipoId");

                    b.Property<int>("idEmisor");

                    b.Property<int>("idTipoMovimiento");

                    b.HasKey("Id");

                    b.HasIndex("EntidadId");

                    b.HasIndex("TipoId");

                    b.HasIndex("idEmisor")
                        .HasName("IX_FK_EmisorGasto");

                    b.HasIndex("idTipoMovimiento")
                        .HasName("IX_FK_TipoMovimientoGasto");

                    b.ToTable("Gastos");
                });

            modelBuilder.Entity("WebApplication1.Modelo.TipoMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("tipoMovimiento")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("TiposMovimiento");
                });

            modelBuilder.Entity("WebApplication1.Modelo.Gasto", b =>
                {
                    b.HasOne("WebApplication1.Modelo.Entidad", "Entidad")
                        .WithMany()
                        .HasForeignKey("EntidadId");

                    b.HasOne("WebApplication1.Modelo.TipoMovimiento", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId");
                });
#pragma warning restore 612, 618
        }
    }
}