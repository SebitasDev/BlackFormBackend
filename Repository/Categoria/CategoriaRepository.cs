using BlackFormBackend.Data;
using BlackFormBackend.Models.Categorias;
using BlackFormBackend.Models.DTOs.Response;
using MongoDB.Driver;

namespace BlackFormBackend.Repository.Categoria;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly MongoDbContext _context;

    public CategoriaRepository(MongoDbContext context)
    {
        _context = context;
    }

    //CREATE

    //Agregar una categoria
    public async Task<Response<dynamic?>> AgregarCategoria(string idUsuario, string idFormulario,
        List<Models.Categorias.Categoria> listCategorias)
    {
        Response<dynamic?> respuesta = new();

        if (string.IsNullOrEmpty(idUsuario) || string.IsNullOrEmpty(idFormulario) || listCategorias == null ||
            listCategorias.Count == 0)
        {
            respuesta.Mensaje = "Parámetros inválidos";
            respuesta.Estado = false;
            return respuesta;
        }

        try
        {
            var buscarFormulario = await _context.Formularios
                .Find(x => x.UsuarioId == idUsuario && x.Id == idFormulario)
                .SingleOrDefaultAsync();

            /*if (buscarFormulario != null)
            {
                var agregarCategorias = Builders<Models.Formulario>.Update
                    .PushEach(f => f.Categoria, listCategorias);

                var resultado = await _context.Formularios
                    .UpdateOneAsync(x => x.Id == idFormulario && x.UsuarioId == idUsuario, agregarCategorias);

                if (resultado.ModifiedCount > 0)
                {
                    respuesta.Mensaje = "Categorías agregadas exitosamente";
                    respuesta.Estado = true;
                }
                else
                {
                    respuesta.Mensaje = "Hubo un fallo al actualizar el formulario";
                    respuesta.Estado = false;
                }
            }
            else
            {
                respuesta.Mensaje = "Verifica que el formulario sí exista";
                respuesta.Estado = false;
            }*/
        }
        catch (Exception e)
        {
            respuesta.Mensaje = "Ha ocurrido un error inesperado";
            respuesta.Estado = false;
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
}