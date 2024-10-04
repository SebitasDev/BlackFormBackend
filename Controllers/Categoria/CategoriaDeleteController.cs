using BlackFormBackend.Repository.Categoria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Categoria;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class CategoriaDeleteController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaDeleteController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }
    
    [HttpDelete("{idUsuario}/{idFormulario}/{idCategoria}")]
    public async Task<ActionResult> EliminarCategoria(string idUsuario, string idFormulario, Guid idCategoria)
    {
        var respuesta = await _categoriaRepository.DeleteCategoria(idUsuario, idFormulario, idCategoria);

        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status201Created, respuesta);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}