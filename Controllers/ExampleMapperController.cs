using AutoMapper;
using BlackFormBackend.Data;
using BlackFormBackend.Models;
using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BlackFormBackend.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class ExampleMapperController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly MongoDbContext _context;
    
    public ExampleMapperController(IMapper mapper, MongoDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> example([FromBody] UsuarioRequestDTO usuarioRequestDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(usuarioRequestDto);
        
        await _context.Usuarios.InsertOneAsync(usuario);
        return Ok(usuario);
    }

    [HttpPost("EXample2")]
    public async Task<ActionResult> ExampleFormulario([FromBody] FormularioRequestDTO formularioRequestDto)
    {

        Models.Formulario formulario = _mapper.Map<Models.Formulario>(formularioRequestDto);
        
        await _context.Formularios.InsertOneAsync(formulario);
        
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ObtenerCategoria()
    {
        Models.Formulario response =
            await _context.Formularios.Find(x => x.Categoria[0].Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")).SingleOrDefaultAsync();

        FormularioResponseDTO formularioResponse = _mapper.Map<FormularioResponseDTO>(response);

        return Ok(formularioResponse);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ObtenerFormulario()
    {
        Models.Formulario response =
            await _context.Formularios.Find(x => x.Id == "66f860398a0246e24c305cae").SingleOrDefaultAsync();

        FormularioResponseDTO formularioResponse = _mapper.Map<FormularioResponseDTO>(response);

        return Ok(formularioResponse);
    }
}

//66f71d3be53a83771a82815d
//3fa85f64-5717-4562-b3fc-2c963f66afa6
//66f860398a0246e24c305cae