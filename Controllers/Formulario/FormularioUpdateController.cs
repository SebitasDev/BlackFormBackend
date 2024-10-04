using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Repository.Formulario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Formulario;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class FormularioUpdateController : ControllerBase
{
    private IFormularioRepository _formularioRepository;

    public FormularioUpdateController(IFormularioRepository formularioRepository)
    {
        _formularioRepository = formularioRepository;
    }

    [HttpPatch("[action]/{idUsuario}/{idFormulario}")]
    public async Task<ActionResult> CambioEstado(string idUsuario, string idFormulario)
    {
        var respuesta = await _formularioRepository.CambioEstado(idFormulario, idUsuario);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
    
    [HttpPatch("[action]/{idUsuario}/{idFormulario}")]
    public async Task<ActionResult> CambioPrivacidad(string idUsuario, string idFormulario)
    {
        var respuesta = await _formularioRepository.CambioPrivacidad(idFormulario, idUsuario);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
    
    [HttpPut("{idUsuario}/{idFormulario}")]
    public async Task<ActionResult> ActualizarFormulario(string idUsuario, string idFormulario, FormularioUpdateRequestDTO formularioUpdateRequestDto)
    {
        var respuesta = await _formularioRepository.EditarFormulario(idFormulario, idUsuario, formularioUpdateRequestDto);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}