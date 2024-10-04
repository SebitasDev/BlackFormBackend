using BlackFormBackend.Models.DTOs.Response;

namespace BlackFormBackend.Repository.Usuario;

public interface IUsuarioRepository
{
    //POST
    Task<Response<dynamic?>> AgregarFormulario(string idUsurio, string idFormulario);
    
    
    //UPDATE
    Task<Response<dynamic?>> EliminarFormulario(string idUsurio, string idFormulario);
}