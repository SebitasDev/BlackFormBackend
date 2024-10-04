using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;

namespace BlackFormBackend.Repository.Categoria;

public interface ICategoriaRepository
{
    //CREATE
    Task<Response<dynamic?>> CreateCategoria(string idUsuario, string idFormulario, CategoriaRequestDTO categoriaRequestDTO);
    
    //UPDATE
    Task<Response<dynamic?>> UpdateCategoria(string idUsuario, string idFormulario, Guid idCategoria, CategoriaUpdateRequestDTO categoriaUpdateRequestDto);
    
    //DELETE
    Task<Response<dynamic?>> DeleteCategoria(string idUsuario, string idFormulario, Guid idCategoria);
}