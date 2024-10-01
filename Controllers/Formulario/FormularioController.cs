using BlackFormBackend.Repository.Formulario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Formulario;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class FormularioController : ControllerBase
{
    private readonly IFormularioRepository _formularioRepository;

    public FormularioController(IFormularioRepository formularioRepository)
    {
        _formularioRepository = formularioRepository;
    }

    [HttpGet("{idUsuario}/{idFormulario}")]
    public async Task<ActionResult> ObtenerFormulario(string idUsuario, string idFormulario)
    {
        var respuesta = await _formularioRepository.FormularioDeUsuario(idUsuario, idFormulario);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }

        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }

    
    [AllowAnonymous]
    [HttpGet("[action]/{idUrl}")]
    public async Task<ActionResult> ObtenerFormularioCompartido(string idUrl)
    {
        var respuesta = await _formularioRepository.FormularioCompartido(idUrl);
        
        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }

        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}