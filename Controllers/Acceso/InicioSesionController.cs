using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Repository.Acceso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Acceso;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class InicioSesionController : ControllerBase
{
    private readonly IAccesoRepository _accesoRepository;

    public InicioSesionController(IAccesoRepository accesoRepository)
    {
        _accesoRepository = accesoRepository;
    }

    [HttpPost]
    public async Task<ActionResult> IniciarSesion([FromBody] LoginRequestDTO loginRequestDto)
    {
        var respuesta = await _accesoRepository.InicioUsuario(loginRequestDto);
        
        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status201Created, respuesta);
        }

        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}