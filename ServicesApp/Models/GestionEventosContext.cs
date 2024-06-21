using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionEventos.ServicesApp.Models;

public partial class GestionEventosContext : DbContext
{
    public GestionEventosContext()
    {
    }

    public GestionEventosContext(DbContextOptions<GestionEventosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<EquipoEvento> EquipoEventos { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Historial> Historials { get; set; }

    public virtual DbSet<IntegrantesEquipo> IntegrantesEquipos { get; set; }

    public virtual DbSet<ParticipanteEvento> ParticipanteEventos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=GestionEventos;Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.EquipoId).HasName("PK__Equipo__DE8A0BDFFB98D270");

            entity.ToTable("Equipo");

            entity.Property(e => e.Institucion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Representante).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.RepresentanteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Equipo__Represen__7B264821");
        });

        modelBuilder.Entity<EquipoEvento>(entity =>
        {
            entity.HasKey(e => e.EquipoEventoId).HasName("PK__EquipoEv__FAE79510BC0E0310");

            entity.ToTable("EquipoEvento");

            entity.HasOne(d => d.Equipo).WithMany(p => p.EquipoEventos)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EquipoEve__Equip__09746778");

            entity.HasOne(d => d.Evento).WithMany(p => p.EquipoEventos)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EquipoEve__Event__0880433F");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.EventoId).HasName("PK__Evento__1EEB59218A736646");

            entity.ToTable("Evento");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FechaFinalizacion).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.LugarEvento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Organizador).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.OrganizadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evento__Organiza__01D345B0");
        });

        modelBuilder.Entity<Historial>(entity =>
        {
            entity.HasKey(e => e.HistorialId).HasName("PK__Historia__9752068F85761DAA");

            entity.ToTable("Historial");

            entity.HasOne(d => d.Evento).WithMany(p => p.Historials)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Event__0D44F85C");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Historials)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Usuar__0C50D423");
        });

        modelBuilder.Entity<IntegrantesEquipo>(entity =>
        {
            entity.HasKey(e => e.IntegrantesEquipoId).HasName("PK__Integran__94EB33729F50B7B6");

            entity.ToTable("IntegrantesEquipo");

            entity.HasOne(d => d.Equipo).WithMany(p => p.IntegrantesEquipos)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Integrant__Equip__7E02B4CC");

            entity.HasOne(d => d.Usuario).WithMany(p => p.IntegrantesEquipos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Integrant__Usuar__7EF6D905");
        });

        modelBuilder.Entity<ParticipanteEvento>(entity =>
        {
            entity.HasKey(e => e.ParticipanteEventoId).HasName("PK__Particip__0DD45189A3CDC2E3");

            entity.ToTable("ParticipanteEvento");

            entity.HasOne(d => d.Evento).WithMany(p => p.ParticipanteEventos)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__Event__04AFB25B");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ParticipanteEventos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__Usuar__05A3D694");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8544DD8E6");

            entity.ToTable("Usuario");

            entity.Property(e => e.Correo).HasMaxLength(255);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Organizacion).HasMaxLength(255);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Profesion).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
