using GestionEventos.ServicesApp.Models;
using GestionEventos.ServicesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]

public class EquipoController: ControllerBase
{
    private readonly GestionEventosContext appDbContext;
    public EquipoController(GestionEventosContext _appDbContext)
    {
        appDbContext = _appDbContext;
    }

    [HttpPost("registro")]
    public IActionResult registrarEquipo([FromBody] EquipoRegistro equipoDatos)
    {
        
        var equipoService = new EquipoService();

        if(equipoService.existeEquipoEnEvento(appDbContext, equipoDatos.Nombre, equipoDatos.EventoId))
        {
            return BadRequest(new {Mensage = "Un equipo con este nombre ya existe"});   
        }
        else if(!equipoService.integrantesRegistrados(appDbContext, equipoDatos))
        {
            return NotFound(new {Mensaje = "Alguno de los integrantes no se encuentra registrado"});
        }

        equipoService.registrarEquipos(appDbContext, equipoDatos, equipoDatos.EventoId);
        
        return Ok();
    }

     [HttpGet("participacion/{id}")]
    public IActionResult getEquiposParticipantes(int id)
    {
        var equipoService = new EquipoService();
        return Ok(equipoService.getEquiposParticipantes(appDbContext, id));
    }


    [HttpGet("lista/final/participacion/{eventoId}")]
    public IActionResult getListaFinalEquiposParticipacion(int eventoId)
    {
        var equipoService = new EquipoService();
        return Ok(equipoService.getEquiposParticipantes(appDbContext, eventoId));
    }
}