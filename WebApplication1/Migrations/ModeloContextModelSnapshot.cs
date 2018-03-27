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
    partial class ModeloContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("Fecha")
                        .HasColumnName("fecha")
                        .HasColumnType("datetime");

                    b.Property<int?>("Fijo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Fijo")
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<double>("Importe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<int>("idEntidad");

                    b.Property<int>("idTipoMovimiento");

                    b.HasKey("Id");

                    b.HasIndex("idEntidad");

                    b.HasIndex("idTipoMovimiento");

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
                        .HasForeignKey("idEntidad")
                        .HasConstraintName("FK_Gasto_Entidad")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication1.Modelo.TipoMovimiento", "Tipo")
                        .WithMany()
                        .HasForeignKey("idTipoMovimiento")
                        .HasConstraintName("FK_Gasto_Tipo")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
