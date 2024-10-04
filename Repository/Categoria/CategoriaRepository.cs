using AutoMapper;
using BlackFormBackend.Data;
using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlackFormBackend.Repository.Categoria;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly IMapper _mapper;
    private readonly MongoDbContext _context;

    public CategoriaRepository(IMapper mapper, MongoDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<dynamic?>> CreateCategoria(string idUsuario, string idFormulario, CategoriaRequestDTO categoriaRequestDTO)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            Models.Categorias.Categoria categoria = _mapper.Map<Models.Categorias.Categoria>(categoriaRequestDTO);

            var agregarCategoria = Builders<Models.Formulario>.Update
                .Push(x => x.Categoria, categoria);
            
            var actualizarFormulario = await _context.Formularios
                .UpdateOneAsync(x => x.Id == idFormulario && x.UsuarioId == idUsuario, agregarCategoria);

            if (actualizarFormulario.ModifiedCount == 0)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se pudo crear la pregunta";
            }
            
            respuesta.Estado = true;
            respuesta.Mensaje = "Se ha creado la pregunta correctamente";
            
        }
        catch (Exception e)
        {
            respuesta.Estado = false;
            respuesta.Mensaje = "Ha ocurrido un error";
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }

    public async Task<Response<dynamic?>> UpdateCategoria(string idUsuario, string idFormulario,
        Guid idCategoria, CategoriaUpdateRequestDTO categoriaUpdateRequestDto)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            var update = Builders<Models.Formulario>.Update
                .Set("Categoria.$[elem].Pregunta", categoriaUpdateRequestDto.Pregunta)
                .Set("Categoria.$[elem].Opciones", categoriaUpdateRequestDto.Opciones);

            var arrayFilters = new[]
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("elem._id", idCategoria))
            };

            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };

            var updateResult = await _context.Formularios
                .UpdateOneAsync(x=> x.Id == idFormulario && x.UsuarioId == idUsuario, update, updateOptions);

            if (updateResult.ModifiedCount > 0)
            {
                respuesta.Estado = true;
                respuesta.Mensaje = "Categoría actualizada exitosamente";
            }
            else
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se encontró la categoría para actualizar";
            }
        }
        catch (Exception e)
        {
            respuesta.Estado = false;
            respuesta.Mensaje = "Ha ocurrido un error inesperado";
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
    
    //DELETE
    public async Task<Response<dynamic?>> DeleteCategoria(string idUsuario, string idFormulario, Guid idCategoria)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            var eliminarCategoria = Builders<Models.Formulario>.Update
                .PullFilter(x => x.Categoria, 
                    x => x.Id == idCategoria);

            var resultadoActualizacion = await _context.Formularios
                .UpdateOneAsync(x => x.Id == idFormulario && x.UsuarioId == idUsuario, eliminarCategoria);

            if (resultadoActualizacion.ModifiedCount > 0)
            {
                respuesta.Estado = true;
                respuesta.Mensaje = "Categoría eliminada exitosamente";
            }
            else
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se encontró la categoría para eliminar";
            }
        }
        catch (Exception e)
        {
            respuesta.Estado = false;
            respuesta.Mensaje = "Ha ocurrido un error inesperado";
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
}