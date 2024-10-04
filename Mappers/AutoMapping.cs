using AutoMapper;
using BlackFormBackend.Models;
using BlackFormBackend.Models.Categorias;
using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;

namespace BlackFormBackend.Mappers;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        /* Usuarios */
        
        //Request -> Model
        CreateMap<UsuarioRequestDTO, Usuario>()
            .ForMember(m => m.Nombre, 
                md => md.MapFrom(d => d.Nombre));

        //Model -> Response
        CreateMap<Usuario, UsuarioResponseDTO>();
        
        /* Formularios */
        
        //Request -> Model
        CreateMap<FormularioRequestDTO, Formulario>();
        
        //Model -> Response
        CreateMap<Formulario, FormularioResponseDTO>();
        
        /* Categoria */
        CreateMap<CategoriaRequestDTO, Categoria>();
    }
}