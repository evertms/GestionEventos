using System;
using System.Collections.Generic;

namespace GestionEventos.ServicesApp.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public string Telefono { get; set; } = null!;

    public string Organizacion { get; set; } = null!;

    public string Profesion { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Equipo> Equipos { get; } = new List<Equipo>();

    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();

    public virtual ICollection<IntegrantesEquipo> IntegrantesEquipos { get; } = new List<IntegrantesEquipo>();

    public virtual ICollection<ParticipanteEvento> ParticipanteEventos { get; } = new List<ParticipanteEvento>();
}

public class UsuarioLogin 
{
    public string? Correo { get; set; }

    public string? Password { get; set; }
}

public class UsuarioEvento
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Organizacion { get; set; }
}

public class UsuarioRegistroEvento 
{
    public int UsuarioId { get; set; }

    public int EventoId { get; set; }
    
    public string correo { get; set; } = null!;
}