using BlackFormBackend.Models.Categorias;
using BlackFormBackend.Models.DTOs.Response;

namespace BlackFormBackend.Repository.Categoria;

public interface ICategoriaRepository
{
    Task<Response<dynamic?>> AgregarCategoria(string idUsuario, string idFormulario, List<Models.Categorias.Categoria> listCategorias);
}