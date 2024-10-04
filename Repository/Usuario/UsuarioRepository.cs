using BlackFormBackend.Custom;
using BlackFormBackend.Data;
using BlackFormBackend.Models.DTOs.Response;
using MongoDB.Driver;

namespace BlackFormBackend.Repository.Usuario;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly MongoDbContext _context;

    public UsuarioRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Response<dynamic?>> AgregarFormulario(string idUsurio, string idFormulario)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            var agregarFormulario = Builders<Models.Usuario>.Update
                .Push(x => x.Formularios, idFormulario);
            
            var update = await _context.Usuarios
                .UpdateOneAsync(x => x.Id == idUsurio, agregarFormulario);

            if (update.ModifiedCount == 0)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se ha podido agregar el formulario";
            }

            respuesta.Estado = true;
            respuesta.Mensaje = "Se agrego correctamente el formulario";
        }
        catch (Exception e)
        {
            respuesta.Estado = false;
            respuesta.Mensaje = "Ha ocurrido un error";
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
    
    public async Task<Response<dynamic?>> EliminarFormulario(string idUsurio, string idFormulario)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            var eliminarFormulario = Builders<Models.Usuario>.Update
                .Pull(x => x.Formularios, idFormulario);
            
            var update = await _context.Usuarios
                .UpdateOneAsync(x => x.Id == idUsurio, eliminarFormulario);

            if (update.ModifiedCount == 0)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se ha podido eliminar el formulario";
            }

            respuesta.Estado = true;
            respuesta.Mensaje = "Se elimino correctamente el formulario";
        }
        catch (Exception e)
        {
            respuesta.Estado = false;
            respuesta.Mensaje = "Ha ocurrido un error";
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
}