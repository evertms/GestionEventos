using System;
using System.Collections.Generic;

namespace GestionEventos.ServicesApp.Models;

public partial class ParticipanteEvento
{
    public int ParticipanteEventoId { get; set; }

    public int UsuarioId { get; set; }

    public int EventoId { get; set; }

    public virtual Evento Evento { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
