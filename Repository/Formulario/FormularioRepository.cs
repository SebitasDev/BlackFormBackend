using AutoMapper;
using BlackFormBackend.Data;
using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;
using BlackFormBackend.Repository.Usuario;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
                .Find(x => x.UsuarioId == idUsuario && x.esActivo == true).ToListAsync();

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

    //Obtener formularios en papelera
    public async Task<Response<dynamic?>> FormulariosEnPapelera(string idUsuario)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            List<Models.Formulario> formularios = await _context.Formularios
                .Find(x => x.UsuarioId == idUsuario && x.esActivo == false)
                .ToListAsync();

            List<FormularioResponseDTO> formularioResponseDtos = [];

            if (formularios.Count > 0)
            {
                foreach (var i in formularios)
                {
                    formularioResponseDtos.Add(_mapper.Map<FormularioResponseDTO>(i));
                }
            }
            
            respuesta.Estado = true;
            respuesta.Mensaje = "Formularios encontrados exitosamente";
            respuesta.Cuerpo = formularioResponseDtos;
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
            respuesta.Estado = true;
            respuesta.Mensaje = "test";
            respuesta.Cuerpo = formulario;
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
                    await _context.Formularios
                        .DeleteOneAsync(x => x.Id == formulario.Id);
                    respuesta.Estado = false;
                    respuesta.Mensaje = resultadoUsuario.Mensaje;
                    respuesta.Cuerpo = resultadoUsuario.Cuerpo;
                }
            }
        }
        catch (Exception e)
        {
            respuesta.Estado = false;
            respuesta.Mensaje = "Ha ocurrido un error inesperado";
            respuesta.Cuerpo = e.ToString();
        }

        return respuesta;
    }
    
    //UPDATE
    public async Task<Response<dynamic?>> CambioEstado(string idFormulario, string idUsuario)
    {
        Response<dynamic> respuesta = new();
        try
        {
            var formulario = await _context.Formularios
                .Find(x => x.Id == idFormulario && x.UsuarioId == idUsuario)
                .SingleOrDefaultAsync();

            if (formulario != null)
            {
                var softDelete = Builders<Models.Formulario>.Update
                    .Set(x => x.esActivo, !formulario.esActivo);

                await _context.Formularios
                    .UpdateOneAsync(x => x.Id == idFormulario && x.UsuarioId == idUsuario,
                        softDelete);
                
                respuesta.Estado = true;
                respuesta.Mensaje = "Estado del formulario cambiado exitosamente";
            }
            else
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se ha podido encontrar el formulario";
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
    
    public async Task<Response<dynamic?>> CambioPrivacidad(string idFormulario, string idUsuario)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            var formulario = await _context.Formularios
                .Find(x => x.Id == idFormulario && x.UsuarioId == idUsuario)
                .SingleOrDefaultAsync();

            if (formulario != null)
            {
                var cambioEstado = Builders<Models.Formulario>.Update
                    .Set(x => x.esPublico, !formulario.esPublico);

                await _context.Formularios
                    .UpdateOneAsync(x => x.Id == idFormulario && x.UsuarioId == idUsuario,
                        cambioEstado);
                
                respuesta.Estado = true;
                respuesta.Mensaje = "Privacidad del formulario cambiado exitosamente";
            }
            else
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se ha podido encontrar el formulario";
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

    public async Task<Response<dynamic?>> EditarFormulario(string idFormulario, string idUsuario,
        FormularioUpdateRequestDTO formularioUpdateRequestDto)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            var editarFormulario = Builders<Models.Formulario>.Update
                .Set(x => x.Nombre, formularioUpdateRequestDto.Nombre)
                .Set(x => x.Descripcion, formularioUpdateRequestDto.Descripcion)
                .Set(x => x.Tematica, formularioUpdateRequestDto.Tematica)
                .Set(x => x.ImagenReferencia, formularioUpdateRequestDto.ImagenReferencia);
            
            var resultadoActualizacion = await _context.Formularios
                .UpdateOneAsync(x => x.Id == idFormulario && x.UsuarioId == idUsuario,
                    editarFormulario);
            
            if (resultadoActualizacion.ModifiedCount > 0)
            {
                respuesta.Estado = true;
                respuesta.Mensaje = "Formulario actualizado exitosamente";
            }
            else
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se encontró el formulario para actualizar";
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
    
    //DELETE
    public async Task<Response<dynamic?>> EliminarFormulario(string idFormulario, string idUsuario)
    {
        Response<dynamic?> respuesta = new();
        try
        {
            var formulario = await _context.Formularios
                .Find(x => x.Id == idFormulario && x.UsuarioId == idUsuario && x.esActivo == false)
                .SingleOrDefaultAsync();

            if (formulario != null)
            {
                await _context.Formularios
                    .DeleteOneAsync(x => x.Id == idFormulario && x.UsuarioId == idUsuario);
                
                var resultadoUsuario = await _usuarioRepository.EliminarFormulario(idUsuario, idFormulario);

                if (resultadoUsuario.Estado)
                {
                    respuesta.Estado = true;
                    respuesta.Mensaje = "Formulario eliminado exitosamente";
                }
                else
                {
                    await _context.Formularios
                        .InsertOneAsync(formulario);
                    respuesta.Estado = false;
                    respuesta.Mensaje = resultadoUsuario.Mensaje;
                    respuesta.Cuerpo = resultadoUsuario.Cuerpo;
                }
            }
            else
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "No se ha podido encontrar el formulario";
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
}