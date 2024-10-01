using AutoMapper;
using BlackFormBackend.Data;
using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;
using BlackFormBackend.Repository.Usuario;
using MongoDB.Driver;

namespace BlackFormBackend.Repository.Formulario;

public class FormularioRepository : IFormularioRepository
{
    private readonly MongoDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;

    public FormularioRepository(MongoDbContext context, IMapper mapper, IUsuarioRepository usuarioRepository)
    {
        _context = context;
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
    }

    //GET
    
    //Obtener varios formularios
    public async Task<Response<dynamic?>> FormulariosDeUnUsuario(string idUsuario)
    {
        Response<dynamic?> response = new();
        try
        {
            List<Models.Formulario> formularios = await _context.Formularios
                .Find(x => x.UsuarioId == idUsuario).ToListAsync();

            if (formularios.Count > 0)
            {
                List<FormularioResponseDTO> formularioResponseDtos = new();
                
                foreach (var i in formularios)
                {
                    formularioResponseDtos.Add(_mapper.Map<FormularioResponseDTO>(i));
                }

                response.Estado = true;
                response.Mensaje = "Formularios encontrados exitosamente";
                response.Cuerpo = formularioResponseDtos!;
            }
            else
            {
                response.Estado = false;
                response.Mensaje = "No se ha podido encontrar ningun formulario";
                response.Cuerpo = null;
            }
        }
        catch (Exception e)
        {
            response.Mensaje = "Ha ocurrido un error inesperado";
            response.Estado = false;
            response.Cuerpo = e.ToString();
        }

        return response;
    }
    
    //Obtener Formulario
    public async Task<Response<dynamic?>> FormularioDeUsuario(string idUsuario, string idFormulario)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            Models.Formulario formulario = await _context.Formularios
                .Find(x => x.Id == idFormulario && x.UsuarioId == idUsuario)
                .SingleOrDefaultAsync();

            if (formulario != null)
            {
                FormularioResponseDTO formularioResponseDto = _mapper.Map<FormularioResponseDTO>(formulario);

                respuesta.Mensaje = "Formulario encontrado exitosamente";
                respuesta.Estado = true;
                respuesta.Cuerpo = formularioResponseDto;
            }
            else
            {
                respuesta.Mensaje = "No se ha podido encontrar ningun formulario";
                respuesta.Estado = false;
            }
        }
        catch (Exception e)
        {
            respuesta.Mensaje = "Ha ocurrido un error inesperado";
            respuesta.Estado = false;
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
    
    //Obtener formulario compartido
    public async Task<Response<dynamic?>> FormularioCompartido(string idUrl)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            Models.Formulario formulario = await _context.Formularios
                .Find(x => x.Url!.Contains(idUrl) && x.esActivo == true && x.esPublico == true)
                .SingleOrDefaultAsync();

            if (formulario != null)
            {
                FormularioResponseDTO formularioResponseDto = _mapper.Map<FormularioResponseDTO>(formulario);

                respuesta.Estado = true;
                respuesta.Mensaje = "Formulario encontrado exitosamente";
                respuesta.Cuerpo = formularioResponseDto;
            }
            else
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "Fallo al encontrar el formulario";
            }
        }
        catch (Exception e)
        {
            respuesta.Mensaje = "Ha ocurrido un error inesperado";
            respuesta.Estado = false;
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
    
    
    //CREATE
    public async Task<Response<dynamic?>> CrearFormulario(FormularioRequestDTO formularioRequestDto)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            Models.Formulario formulario = _mapper.Map<Models.Formulario>(formularioRequestDto);

            await _context.Formularios.InsertOneAsync(formulario);

            if (formulario.Id != null)
            {
                var resultadoUsuario = await _usuarioRepository.AgregarFormulario(formulario.UsuarioId, formulario.Id);

                if (resultadoUsuario.Estado)
                {
                    respuesta.Estado = true;
                    respuesta.Mensaje = "Formulario creado exitosamente";
                    respuesta.Cuerpo = formulario.Id;
                }
                else
                {
                    respuesta.Estado = false;
                    respuesta.Mensaje = resultadoUsuario.Mensaje;
                    respuesta.Cuerpo = resultadoUsuario.Cuerpo;
                }
            }
        }
        catch (Exception e)
        {
            respuesta.Estado = false;
            respuesta.Mensaje = "Formulario creado exitosamente";
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
    
}