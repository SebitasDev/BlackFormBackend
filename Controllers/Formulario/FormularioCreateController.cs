using BlackFormBackend.Data;
using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Repository.Formulario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Formulario;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class FormularioCreateController : ControllerBase
{
    private readonly IFormularioRepository _formularioRepository;

    public FormularioCreateController(IFormularioRepository formularioRepository)
    {
        _formularioRepository = formularioRepository;
    }

    [HttpPost]
    public async Task<ActionResult> CrearFormulario([FromBody] FormularioRequestDTO formularioRequestDto)
    {
        var respuesta = await _formularioRepository.CrearFormulario(formularioRequestDto);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status201Created, respuesta);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}