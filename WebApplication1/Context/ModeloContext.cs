using Microsoft.EntityFrameworkCore;
using WebApplication1.Modelo;

namespace WebApplication1.Context
{
    public class ModeloContext : DbContext
    {
        public ModeloContext(DbContextOptions<ModeloContext> options)
            : base(options) { }

        public DbSet<TipoMovimiento> TiposMovimiento { get; set; }
        public DbSet<Entidad> Entidades { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<FijosMes> FijosMes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entidad>(entity =>
            {
                entity.Property(e => e.nombreEntidad).IsRequired();
            });

            modelBuilder.Entity<TipoMovimiento>(entity =>
            {
                entity.Property(e => e.tipoMovimiento).IsRequired();
            });

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.Activo)
                    .HasColumnType("int")
                    .HasColumnName("Activo")
                    .HasDefaultValue(0);

                entity.Property(e => e.Fijo)
                    .HasColumnType("int")
                    .HasColumnName("Fijo")
                    .HasDefaultValue(0);

                entity.Property(e => e.Importe)
                    .HasColumnType("float")
                    .HasDefaultValue(0);

                entity.HasOne(e => e.Entidad)
                    .WithMany()
                    .HasForeignKey(e => e.idEntidad)
                    .HasConstraintName("FK_Gasto_Entidad")
                    .IsRequired();

                entity.HasOne(e => e.Tipo)
                    .WithMany()
                    .HasForeignKey(e => e.idTipoMovimiento)
                    .HasConstraintName("FK_Gasto_Tipo")
                    .IsRequired();
            });

            modelBuilder.Entity<FijosMes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .UseSqlServerIdentityColumn();

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.Importe)
                    .HasColumnType("float")
                    .HasDefaultValue(0);

                entity.HasOne(e => e.Entidad)
                    .WithMany()
                    .HasForeignKey(e => e.idEntidad)
                    .HasConstraintName("FK_Gasto_Entidad")
                    .IsRequired();
            });

            modelBuilder.Entity<FijosMes>().HasAlternateKey(c => new { c.Fecha, c.idEntidad }).HasName("IX_FechaIdentidad");
        }
    }
}
