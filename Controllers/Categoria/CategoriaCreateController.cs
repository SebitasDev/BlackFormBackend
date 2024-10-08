using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Repository.Categoria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Categoria;

[Route("api/categoria-create")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class CategoriaCreateController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaCreateController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpPost("{idUsuario}/{idFormulario}")]
    public async Task<ActionResult> CrearFormulario([FromBody] CategoriaRequestDTO categoriaRequestDto,
        string idUsuario, string idFormulario)
    {
        var respuesta = await _categoriaRepository.CreateCategoria(idUsuario, idFormulario, categoriaRequestDto);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status201Created, respuesta);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}