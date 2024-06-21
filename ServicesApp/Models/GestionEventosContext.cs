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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=GestionEventos;Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.EquipoId).HasName("PK__Equipo__DE8A0BDFB7ABDA99");

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
                .HasConstraintName("FK__Equipo__Represen__6442E2C9");
        });

        modelBuilder.Entity<EquipoEvento>(entity =>
        {
            entity.HasKey(e => e.EquipoEventoId).HasName("PK__EquipoEv__FAE79510AAC8019E");

            entity.ToTable("EquipoEvento");

            entity.HasOne(d => d.Equipo).WithMany(p => p.EquipoEventos)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EquipoEve__Equip__72910220");

            entity.HasOne(d => d.Evento).WithMany(p => p.EquipoEventos)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EquipoEve__Event__719CDDE7");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.EventoId).HasName("PK__Evento__1EEB5921B7CEBF41");

            entity.ToTable("Evento");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FechaFinalizacion).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.Inicio).HasColumnType("date");
            entity.Property(e => e.LugarEvento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Organizador).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.OrganizadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evento__Organiza__6AEFE058");
        });

        modelBuilder.Entity<Historial>(entity =>
        {
            entity.HasKey(e => e.HistorialId).HasName("PK__Historia__9752068FAACA3946");

            entity.ToTable("Historial");

            entity.HasOne(d => d.Evento).WithMany(p => p.Historials)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Event__76619304");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Historials)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Usuar__756D6ECB");
        });

        modelBuilder.Entity<IntegrantesEquipo>(entity =>
        {
            entity.HasKey(e => e.IntegrantesEquipoId).HasName("PK__Integran__94EB337224677540");

            entity.ToTable("IntegrantesEquipo");

            entity.HasOne(d => d.Equipo).WithMany(p => p.IntegrantesEquipos)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Integrant__Equip__671F4F74");

            entity.HasOne(d => d.Usuario).WithMany(p => p.IntegrantesEquipos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Integrant__Usuar__681373AD");
        });

        modelBuilder.Entity<ParticipanteEvento>(entity =>
        {
            entity.HasKey(e => e.ParticipanteEventoId).HasName("PK__Particip__0DD451899F002E0B");

            entity.ToTable("ParticipanteEvento");

            entity.HasOne(d => d.Evento).WithMany(p => p.ParticipanteEventos)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__Event__6DCC4D03");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ParticipanteEventos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__Usuar__6EC0713C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8AC142397");

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
