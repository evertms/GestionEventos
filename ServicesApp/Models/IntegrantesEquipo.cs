using System;
using System.Collections.Generic;

namespace GestionEventos.ServicesApp.Models;

public partial class IntegrantesEquipo
{
    public int IntegrantesEquipoId { get; set; }

    public int EquipoId { get; set; }

    public int UsuarioId { get; set; }

    public virtual Equipo Equipo { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
