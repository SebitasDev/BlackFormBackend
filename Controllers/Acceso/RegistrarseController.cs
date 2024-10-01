using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;
using BlackFormBackend.Repository.Acceso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Acceso;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class RegistrarseController : ControllerBase
{
    private readonly IAccesoRepository _accesoRepository;

    public RegistrarseController(IAccesoRepository accesoRepository)
    {
        _accesoRepository = accesoRepository;
    }
    
    [HttpPost]
    public async Task<ActionResult> RegistrarUsuario([FromBody] UsuarioRequestDTO usuarioRequestDto)
    {
        var respuesta = await _accesoRepository.RegistroUsuario(usuarioRequestDto);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }

        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    } 
}