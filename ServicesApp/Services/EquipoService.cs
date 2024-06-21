using System.Xml.Schema;
using GestionEventos.ServicesApp.Models;

namespace GestionEventos.ServicesApp.Services;

public class EquipoService 
{
    public bool existeEquipoEnEvento(GestionEventosContext appDbContext, string? nombreEquipo, int eventoId)
    {
        var team = (from equipo in appDbContext.Equipos
                      join eventoEquipo in appDbContext.EquipoEventos
                      on equipo.EquipoId equals eventoEquipo.EquipoId
                      where eventoEquipo.EventoId == eventoId &&
                      equipo.Nombre == nombreEquipo
                      select equipo
        ).FirstOrDefault();

        return team != null;
    }

    public List<int> getIDsEquipo(GestionEventosContext appDbContext,List<string?> correos)
    {
        var identificadores = (from usuario in appDbContext.Usuarios
                               where correos.Contains(usuario.Correo)
                               select usuario.UsuarioId
        ).ToList();

        return identificadores;
    }

    public List<string?> getCorreosEquipo(List<IntegranteEquipoDatos> integranteEquipoDatos)
    {
        List<string?> correos = new List<string?>();

        foreach (var integranteEquipoDato in integranteEquipoDatos)
        {
            correos.Add(integranteEquipoDato.Correo);
        }

        return correos;
    }

    public bool integrantesRegistrados(GestionEventosContext appDbContext, EquipoRegistro equipoRegistro)
    {
        List<string?> correos = this.getCorreosEquipo(equipoRegistro.datosEquipo);
        var IDs = this.getIDsEquipo(appDbContext, correos);

        return IDs.Count == equipoRegistro.datosEquipo.Count;
    }

    public List<InfoParticipacionEquipo> getEquiposParticipantes(GestionEventosContext appDbContext, int eventoId)
    {
        var equipos = (from equipo in appDbContext.Equipos
                       join equipoEvento in appDbContext.EquipoEventos
                       on equipo.EquipoId equals equipoEvento.EquipoId
                       where equipoEvento.EventoId == eventoId
                       orderby equipo.Nombre
                       select new InfoParticipacionEquipo {
                        EquipoId = equipo.EquipoId,
                        Nombre = equipo.Nombre,
                        NumeroIntegrantes = equipo.NumIntegrantes,
                        Organizacion = equipo.Institucion,
                        RepresentanteId = equipo.RepresentanteId,
                       
        }).ToList();

        return equipos;
    }

    public void registrarEquipos(GestionEventosContext appDbContext, EquipoRegistro equipoRegistro, int eventoId)
    {
        List<string?> correos = this.getCorreosEquipo(equipoRegistro.datosEquipo);
        List<int> IDs = this.getIDsEquipo(appDbContext, correos);

        var equipo = new Equipo {
            Nombre = equipoRegistro.Nombre,
            NumIntegrantes = equipoRegistro.datosEquipo.Count,
            Institucion = equipoRegistro.Organizacion,
            RepresentanteId = equipoRegistro.RepresentanteId
        };

        appDbContext.Equipos.Add(equipo);
        appDbContext.SaveChanges();

        int equipoId = equipo.EquipoId;

        var equipoEvento = new EquipoEvento {
            EquipoId = equipoId,
            EventoId = eventoId,
        };

        appDbContext.Add(equipoEvento);
        appDbContext.SaveChanges();

        List<IntegrantesEquipo> integrantesEquipo = new List<IntegrantesEquipo>();

        for (int i = 0; i < IDs.Count; i++) 
        {
            var temp = new IntegrantesEquipo {
                IntegrantesEquipoId = IDs[i],
                EquipoId = equipoId
            };

            integrantesEquipo.Add(temp);
        }

        appDbContext.IntegrantesEquipos.AddRange(integrantesEquipo);
        appDbContext.SaveChanges();
    }

    public string? getNombreEquipo(GestionEventosContext appDbContext, int eventoId)
    {
        var nombreEquipo = (from evento in appDbContext.Eventos
                            where evento.EventoId == eventoId
                            select evento.Titulo
        ).FirstOrDefault();

        return nombreEquipo;
    }
}