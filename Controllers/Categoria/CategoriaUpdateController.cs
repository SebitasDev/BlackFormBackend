using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Repository.Categoria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Categoria;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class CategoriaUpdateController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaUpdateController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpPut("{idUsuario}/{idFormulario}/{idCategoria}")]
    public async Task<ActionResult> ActualizarCategoria([FromBody] CategoriaUpdateRequestDTO categoriaUpdateRequestDto,
        string idUsuario, string idFormulario, Guid idCategoria)
    {
        var respuesta =
            await _categoriaRepository.UpdateCategoria(idUsuario, idFormulario, idCategoria, categoriaUpdateRequestDto);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status201Created, respuesta);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}