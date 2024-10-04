using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;

namespace BlackFormBackend.Repository.Formulario;

public interface IFormularioRepository
{
    //GET
    Task<Response<dynamic?>> FormulariosDeUnUsuario(string idUsuario);
    Task<Response<dynamic?>> FormularioDeUsuario(string idUsuario, string idFormulario);
    Task<Response<dynamic?>> FormularioCompartido(string idUrl);
    Task<Response<dynamic?>> FormulariosEnPapelera(string idUsuario);

    //CREATE
    Task<Response<dynamic?>> CrearFormulario(FormularioRequestDTO formularioRequestDto);

    //UPDATE
    Task<Response<dynamic?>> CambioEstado(string idFormulario, string idUsuario);
    Task<Response<dynamic?>> CambioPrivacidad(string idFormulario, string idUsuario);
    Task<Response<dynamic?>> EditarFormulario(string idFormulario, string idUsuario,
        FormularioUpdateRequestDTO formularioUpdateRequestDto);
    
    //DELETE
    Task<Response<dynamic?>> EliminarFormulario(string idFormulario, string idUsuario);
    

}