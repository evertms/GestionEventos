using System;
using System.Collections.Generic;

namespace GestionEventos.ServicesApp.Models;

public partial class EquipoEvento
{
    public int EquipoEventoId { get; set; }

    public int EventoId { get; set; }

    public int EquipoId { get; set; }

    public virtual Equipo Equipo { get; set; } = null!;

    public virtual Evento Evento { get; set; } = null!;
}
