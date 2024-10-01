using BlackFormBackend.Models.DTOs.Response;

namespace BlackFormBackend.Repository.Usuario;

public interface IUsuarioRepository
{
    Task<Response<dynamic?>> AgregarFormulario(string idUsurio, string idFormulario);
}