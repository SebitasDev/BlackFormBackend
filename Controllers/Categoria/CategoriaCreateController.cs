using BlackFormBackend.Models.Categorias;
using BlackFormBackend.Repository.Categoria;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackFormBackend.Controllers.Categoria;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class CategoriaCreateController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaCreateController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpPut("{idUsuario}/{idFormulario}")]
    public async Task<ActionResult> AgregarCategoria([FromBody] List<Models.Categorias.Categoria> listaCategorias
        ,string idUsuario, string idFormulario)
    {
        var respuesta = await _categoriaRepository.AgregarCategoria(idUsuario, idFormulario,
            listaCategorias);
        
        if (respuesta.Estado)
        {
            return StatusCode(StatusCodes.Status200OK, respuesta);
        }

        return StatusCode(StatusCodes.Status400BadRequest, respuesta);
    }
}