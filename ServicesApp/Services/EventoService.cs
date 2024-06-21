using GestionEventos.ServicesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionEventos.ServicesApp.Services;

public class EventoService 
{
    private readonly GestionEventosContext appDbContext;

    public EventoService(GestionEventosContext _appDbContext) 
    {
        appDbContext = _appDbContext;
    }
    public IEnumerable<Evento> GetEventosIndividuales(int userId)
    {
        DateTime fechaActual = DateTime.Now;
        var eventosIndividuales = appDbContext.Eventos.Where(
            evento => evento.PorEquipos == false &&
            evento.FechaFinalizacion > fechaActual &&
            evento.OrganizadorId != userId
        ).Include(
            evento => evento.Organizador
        ).ToList();

        return eventosIndividuales;
    }

    public IEnumerable<Evento> GetEventosGrupales(int userId)
    {
        DateTime fechaActual = DateTime.Now;
        var eventosGrupales = appDbContext.Eventos.Where(
            evento => evento.PorEquipos == true &&
            evento.FechaFinalizacion > fechaActual &&
            evento.OrganizadorId != userId
        ).Include(
            evento => evento.Organizador
        ).ToList();

        return eventosGrupales;
    }

    public Evento detallesEvento(int eventoId) 
    {
        var detallesEvento = appDbContext.Eventos.Where(
            evento => evento.EventoId == eventoId
        ).Include(
            evento => evento.Organizador
        ).First();

        return detallesEvento;
    }

    public IEnumerable<Evento> getEventosPorOrganizador(int organizadorId)
    {
        DateTime fechaActual = DateTime.Now;
        var eventos = appDbContext.Eventos.Where(
            eventos => eventos.OrganizadorId == organizadorId &&
            eventos.FechaFinalizacion > fechaActual
         ).ToList();

         return eventos;
    }

    public IEnumerable<Evento> getHistorialEventosPorParticipante(int participanteId)
    {
        DateTime fechaActual = DateTime.Now;
        var eventos = (from evento in appDbContext.Eventos
                       join participanteEvento in appDbContext.ParticipanteEventos
                       on evento.EventoId equals participanteEvento.EventoId
                       where participanteEvento.ParticipanteEventoId == participanteId &&
                       evento.FechaFinalizacion <= fechaActual
                       select evento
        ).ToList();

        return eventos;
    }

    public IEnumerable<Evento> getHistorialEventosPorEquipo(int participanteId)
    {
        DateTime fechaActual = DateTime.Now;
        var eventos = (from evento in appDbContext.Eventos
                       join equipoEvento in appDbContext.EquipoEventos
                       on evento.EventoId equals equipoEvento.EventoId
                       join integranteEquipo in appDbContext.IntegrantesEquipos
                       on equipoEvento.EquipoId equals integranteEquipo.EquipoId
                       where integranteEquipo.UsuarioId == participanteId &&
                       evento.FechaFinalizacion <= fechaActual
                       select evento
        ).ToList();

        return eventos;
    }

    public IEnumerable<Evento> getHistorialEventosPorOrganizador(int participanteId)
    {
        DateTime fechaActual = DateTime.Now;
        var eventos = (from evento in appDbContext.Eventos
                       where evento.OrganizadorId == participanteId &&
                       evento.FechaFinalizacion <= fechaActual
                       select evento
        ).ToList();

        return eventos;
    }

    public void crearEvento(Evento evento) 
    {
        appDbContext.Eventos.Add(evento);
        appDbContext.SaveChanges();
    }

    public void registrarParticipante(UsuarioRegistroEvento usuario) 
    {
        var user = new ParticipanteEvento{
            ParticipanteEventoId = usuario.UsuarioId,
            EventoId = usuario.EventoId,
        };

        var nombreEvento = (from evento in appDbContext.Eventos
                            where evento.EventoId == user.EventoId
                            select evento.Titulo
        ).FirstOrDefault();

        appDbContext.ParticipanteEventos.Add(user);
        appDbContext.SaveChanges();
    }

    public bool ParticipanteEventoExists(int participanteId, int eventoId) 
    {
        var participant = (from participante in appDbContext.ParticipanteEventos
                                  where participante.UsuarioId == participanteId &&
                                  participante.EventoId == eventoId
                                  select participante

        ).FirstOrDefault();

        if (participant == null) return false;
        else return true;
    }
}