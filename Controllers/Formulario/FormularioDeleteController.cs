using BlackFormBackend.Repository.Formulario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Formulario;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class FormularioDeleteController : ControllerBase
{
    private readonly IFormularioRepository _formularioRepository;

    public FormularioDeleteController(IFormularioRepository formularioRepository)
    {
        _formularioRepository = formularioRepository;
    }

    [HttpDelete("{idUsuario}/{idFormulario}")]
    public async Task<ActionResult> Formulario(string idUsuario, string idFormulario)
    {
        var respuesta = await _formularioRepository.EliminarFormulario(idFormulario, idUsuario);
        
        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}