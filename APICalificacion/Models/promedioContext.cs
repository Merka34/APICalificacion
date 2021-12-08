using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace APICalificacion.Models
{
    public partial class promedioContext : DbContext
    {
        public promedioContext()
        {
        }

        public promedioContext(DbContextOptions<promedioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Materia> Materia { get; set; }
        public virtual DbSet<Nombremateria> Nombremateria { get; set; }
        public virtual DbSet<Usuarioalumno> Usuarioalumno { get; set; }
        public virtual DbSet<Usuariodocente> Usuariodocente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("alumno");

                entity.Property(e => e.NombreAlumno)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.ToTable("calificacion");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Pf).HasColumnName("PF");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Calificacion)
                    .HasForeignKey<Calificacion>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkMateria");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.ToTable("materia");

                entity.HasIndex(e => e.IdAlumno, "fkAlumno_idx");

                entity.HasIndex(e => e.IdNombreMateria, "fkNombre_idx");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Materia)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkAlumno");

                entity.HasOne(d => d.IdNombreMateriaNavigation)
                    .WithMany(p => p.Materia)
                    .HasForeignKey(d => d.IdNombreMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkNombre");
            });

            modelBuilder.Entity<Nombremateria>(entity =>
            {
                entity.ToTable("nombremateria");

                entity.Property(e => e.NombreMateria1)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("NombreMateria");
            });

            modelBuilder.Entity<Usuarioalumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno)
                    .HasName("PRIMARY");

                entity.ToTable("usuarioalumno");

                entity.HasIndex(e => e.IdAlumno, "fkNombreUsuario_idx");

                entity.Property(e => e.IdAlumno)
                    .ValueGeneratedNever()
                    .HasColumnName("idAlumno");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("contrasena");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithOne(p => p.Usuarioalumno)
                    .HasForeignKey<Usuarioalumno>(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkNombreUsuario");
            });

            modelBuilder.Entity<Usuariodocente>(entity =>
            {
                entity.ToTable("usuariodocente");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
