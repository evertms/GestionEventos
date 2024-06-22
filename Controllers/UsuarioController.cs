using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using GestionEventos.ServicesApp.Models;
using GestionEventos.ServicesApp.Services;

[ApiController]
[Route("[controller]")]

public class UsuarioController: ControllerBase
{

    private readonly GestionEventosContext appDbContext;
    public UsuarioController(GestionEventosContext _appDbContext){
        appDbContext = _appDbContext;
    }

    [HttpPost]
    public IActionResult Autenticar([FromBody] UsuarioLogin user)
    {
        if (user == null) return BadRequest();

        var verificacion = new UsuarioService();
        
        if(!verificacion.Authentication(appDbContext, user))
        {
            return NotFound(new {Mensaje = "Usuario no encontrado"});
        }

        return Ok(verificacion.GetUsuario(appDbContext, user.Correo, user.Password));
    }


    [HttpGet("participantes/{id}")]
    public IActionResult getPartipanteEvento(int id)
    {
        var usuarioService = new UsuarioService();

        return Ok(usuarioService.getParticipantes(appDbContext, id)); 
    }

    
    [HttpPost("signup")]
    public IActionResult validarUsuario([FromBody] Usuario usuario)
    {
        var usuarioService = new UsuarioService();

        if (usuarioService.emailEnUso(appDbContext, usuario.Correo))
        {
            return BadRequest(new {Mensaje = "El correo ya está en uso"});
        }

        appDbContext.Usuarios.Add(usuario);
        appDbContext.SaveChanges();

        return Ok(new {Mensaje = "Registrado Exitosamente"});
    }
}