using System;
using System.Collections.Generic;

namespace GestionEventos.ServicesApp.Models;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string Nombre { get; set; } = null!;

    public int? NumIntegrantes { get; set; }

    public string Institucion { get; set; } = null!;

    public int RepresentanteId { get; set; }

    public virtual ICollection<EquipoEvento> EquipoEventos { get; } = new List<EquipoEvento>();

    public virtual ICollection<IntegrantesEquipo> IntegrantesEquipos { get; } = new List<IntegrantesEquipo>();

    public virtual Usuario Representante { get; set; } = null!;
}
