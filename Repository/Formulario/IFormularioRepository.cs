using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;

namespace BlackFormBackend.Repository.Formulario;

public interface IFormularioRepository
{
    //GET
    Task<Response<dynamic?>> FormulariosDeUnUsuario(string idUsuario);
    Task<Response<dynamic?>> FormularioDeUsuario(string idUsuario, string idFormulario);
    Task<Response<dynamic?>> FormularioCompartido(string idUrl);
    
    //CREATE
    Task<Response<dynamic?>> CrearFormulario(FormularioRequestDTO formularioRequestDto);
}