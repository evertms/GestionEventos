using System;
using System.Collections.Generic;

namespace GestionEventos.ServicesApp.Models;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string Nombre { get; set; } = null!;

    public int NumIntegrantes { get; set; }

    public string Institucion { get; set; } = null!;

    public int RepresentanteId { get; set; }

    public virtual ICollection<EquipoEvento> EquipoEventos { get; } = new List<EquipoEvento>();

    public virtual ICollection<IntegrantesEquipo> IntegrantesEquipos { get; } = new List<IntegrantesEquipo>();

    public virtual Usuario Representante { get; set; } = null!;
}

public class EquipoRegistro 
{
    public string Nombre { get; set; } = null!;

    public string Organizacion {get; set;} = null!;

    public List<IntegranteEquipoDatos> datosEquipo { get; set; } = null!;

    public int EventoId { get; set; }

    public int RepresentanteId { get; set; }

    public string? RepresentanteCorreo { get; set; }
}

public class IntegranteEquipoDatos
{
    public string? Correo { get; set; }

    public string? Nombre { get; set; }
}

public class InfoParticipacionEquipo
{
    public int EquipoId { get; set; }

    public string Nombre { get; set; } = null!;

    public int NumeroIntegrantes { get; set; }

    public string Organizacion { get; set; } = null!;

    public int RepresentanteId { get; set; }
}