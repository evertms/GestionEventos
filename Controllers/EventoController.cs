using Microsoft.AspNetCore.Mvc;
using GestionEventos.ServicesApp.Services;
using GestionEventos.ServicesApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

[ApiController]
[Route("[controller]")]

public class EventoController : ControllerBase
{
    private readonly EventoService eventoService;
    public EventoController(EventoService _eventoService)
    {
        this.eventoService = _eventoService;
    }

    [HttpGet("individual/{usuarioId}")]
    public IActionResult getEventosIndividual(int usuarioId)
    {
        return Ok(this.eventoService.GetEventosIndividuales(usuarioId));
    }
    
    [HttpGet("grupal/{usuarioId}")]
    public IActionResult getEventosGrupales(int usuarioId)
    {
        return Ok(this.eventoService.GetEventosGrupales(usuarioId));
    }

    [HttpGet("detalle/{eventoId}")]
    public IActionResult getDetalleEvento(int eventoId)
    {
        return Ok(this.eventoService.detallesEvento(eventoId));
    }

    [HttpGet("{organizadorId}")]
    public IActionResult getEventoPorOrganizador(int organizadorId)
    {
        return Ok(this.eventoService.getEventosPorOrganizador(organizadorId));
    }

    [HttpGet("organizador/{id}")]
    public IActionResult getHistorialEventoOrganizador(int id)
    {
        return Ok(this.eventoService.getHistorialEventosPorOrganizador(id));
    }

    [HttpGet("participante/{id}")]
    public IActionResult getHistorialEventosParticipante(int id)
    {
        return Ok(this.eventoService.getHistorialEventosPorParticipante(id));
    }

    [HttpPost("crearEvento")]
    public IActionResult newEvento([FromBody]Evento evento)
    {
        var e = new Evento {
            Titulo = evento.Titulo,
            Descripcion = evento.Descripcion,
            FechaInicio = evento.FechaInicio,
            FechaFinalizacion = evento.FechaFinalizacion,
            LugarEvento = evento.LugarEvento,
            PorEquipos = evento.PorEquipos,
            Cupos = evento.Cupos,
            OrganizadorId = evento.OrganizadorId,
        };
        this.eventoService.crearEvento(e);
        return Ok();
    }

    [HttpPost("registro/participante")]
    public IActionResult registroParticipante([FromBody] UsuarioRegistroEvento usuario)
    {
        this.eventoService.registrarParticipante(usuario);
        return Ok();
    }

    [HttpPost("verificar/registro")]
    public IActionResult verificarParticipanteRegistro([FromBody] UsuarioRegistroEvento usuario)
    {
        if (this.eventoService.ParticipanteEventoExists(usuario.UsuarioId, usuario.EventoId))
        {
            return Ok(true);
        } else 
        {
            return Ok(false);
        }
    }

    [HttpPut("lista/participacion/{eventoId}")]
    public IActionResult updateListaParticipacion(int eventoId, [FromBody] List<UsuarioEvento> usuarios)
    {
        var usuarioService = new UsuarioService();
        var context = new GestionEventosContext();
        List<ParticipanteEvento> participantes;
      
        participantes = (from participanteEvento in context.ParticipanteEventos
                         join usuario in context.Usuarios 
                         on participanteEvento.UsuarioId equals usuario.UsuarioId
                         where participanteEvento.EventoId == eventoId
                         orderby usuario.Nombre
                         select participanteEvento).ToList();
        
        context.SaveChanges();

        return Ok();
    }

    [HttpPut("lista/participacion/equipo/{eventoId}")]
    public IActionResult updateListaParticipacionEquipo(int eventoId, [FromBody] List<InfoParticipacionEquipo> equipos)
    {
        var usuarioService = new UsuarioService();
        var context = new GestionEventosContext();
        List<EquipoEvento> equiposParticipantes;

      
        equiposParticipantes = (from equipoEvento in context.EquipoEventos
                                join equipo in context.Equipos on equipoEvento.EquipoId equals equipo.EquipoId
                                where equipoEvento.EventoId == eventoId
                                orderby equipo.Nombre
                                select equipoEvento).ToList();
    
        context.SaveChanges();

        return Ok();
    }
}