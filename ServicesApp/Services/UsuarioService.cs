using System.Diagnostics.Tracing;
using GestionEventos.ServicesApp.Models;

namespace GestionEventos.ServicesApp.Services;

public class UsuarioService 
{
    public bool Authentication(GestionEventosContext appDbContext, UsuarioLogin usuario)
    {
        var usuarioActual = appDbContext.Usuarios.FirstOrDefault(
            x => x.Correo == usuario.Correo &&
            x.Password == usuario.Password
        );

        if (usuarioActual == null) return false;
        else return true;
    }

    public Usuario? GetUsuario(GestionEventosContext appDbContext, string? correo, string? password)
    {
        var usuario = appDbContext.Usuarios.Where(
            x => x.Correo == correo &&
            x.Password == password
        ).FirstOrDefault();

        return usuario;
    }

    public List<UsuarioEvento>? getParticipantes(GestionEventosContext appDbContext, int eventoId) 
    {
        var usuarios = from usuario in appDbContext.Usuarios
                       join participante in appDbContext.ParticipanteEventos
                       on usuario.UsuarioId equals participante.UsuarioId
                       where participante.EventoId == eventoId
                       orderby usuario.Nombre
                       select new UsuarioEvento{
                            UsuarioId = usuario.UsuarioId,
                            Nombre = usuario.Nombre,
                            Correo = usuario.Correo,
                            Organizacion = usuario.Organizacion,
                       };

        return usuarios.ToList();
    }

    public bool emailEnUso(GestionEventosContext appDbContext, string correo)
    {
        var usuario = (from user in appDbContext.Usuarios
                       where user.Correo == correo
                       select user).FirstOrDefault();
        
        if (usuario == null) return false;
        else return true;
    }

    /*
    public List<UsuarioEvento> getHistorialUsuario (GestionEventosContext appDbContext, int eventoId)
    {
         var usuarios = from usuario in appDbContext.Usuarios
                       join participante in appDbContext.ParticipanteEventos
                       on usuario.UsuarioId equals participante.UsuarioId
                       where participante.EventoId == eventoId
                       orderby usuario.Nombre
                       select new UsuarioEvento{
                            UsuarioId = usuario.UsuarioId,
                            Nombre = usuario.Nombre,
                            Correo = usuario.Correo,
                            Organizacion = usuario.Organizacion,
                       };

        return usuarios.ToList();
    }
    */
}