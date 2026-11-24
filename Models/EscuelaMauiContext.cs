using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApp.Models;

public partial class EscuelaMauiContext : DbContext
{
    public EscuelaMauiContext()
    {
    }

    public EscuelaMauiContext(DbContextOptions<EscuelaMauiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumno { get; set; }

    public virtual DbSet<Calificacion> Calificacion { get; set; }

    public virtual DbSet<Carrera> Carrera { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MHERNANDEZ;Database=EscuelaMaui;User Id=appmaui;Password=Maui#2026;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.NoControl).HasName("PK__Alumno__640039B6F66FBA5D");

            entity.Property(e => e.NoControl)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Alumno)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Alumno_Carrera");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.CalificacionId).HasName("PK__Califica__4CF54ADE5D43230D");

            entity.Property(e => e.Materia)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.NoControl)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(4, 1)");

            entity.HasOne(d => d.NoControlNavigation).WithMany(p => p.Calificacion)
                .HasForeignKey(d => d.NoControl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calificacion_Alumno");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carrera__3214EC07ED2CE310");

            entity.Property(e => e.Clave)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
