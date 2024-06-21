using System;
using System.Collections.Generic;

namespace GestionEventos.ServicesApp.Models;

public partial class Evento
{
    public int EventoId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFinalizacion { get; set; }

    public string LugarEvento { get; set; } = null!;

    public int OrganizadorId { get; set; }

    public bool PorEquipos { get; set; }

    public int Cupos { get; set; }

    public virtual ICollection<EquipoEvento> EquipoEventos { get; } = new List<EquipoEvento>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();

    public virtual Usuario Organizador { get; set; } = null!;

    public virtual ICollection<ParticipanteEvento> ParticipanteEventos { get; } = new List<ParticipanteEvento>();
}
