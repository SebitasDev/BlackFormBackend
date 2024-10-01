using AutoMapper;
using BlackFormBackend.Custom;
using BlackFormBackend.Data;
using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;
using BlackFormBackend.Repository.Formulario;
using Microsoft.AspNetCore.Identity.Data;
using MongoDB.Driver;

namespace BlackFormBackend.Repository.Acceso;

public class AccesoRepository : IAccesoRepository
{
    private readonly MongoDbContext _context;
    private readonly Utilidades _utilidades;
    private readonly IMapper _mapper;
    private readonly IFormularioRepository _formularioRepository;


    public AccesoRepository(MongoDbContext context, Utilidades utilidades, IMapper mapper, IFormularioRepository formularioRepository)
    {
        _context = context;
        _utilidades = utilidades;
        _mapper = mapper;
        _formularioRepository = formularioRepository;
    }

    public async Task<Response<string?>> RegistroUsuario(UsuarioRequestDTO usuarioRequestDto)
    {
        Response<string?> response = new();
        response.Cuerpo = null;
        try
        {
            Models.Usuario usuario = _mapper.Map<Models.Usuario>(usuarioRequestDto);
            usuario.Contrasena = _utilidades.EncriptarSHA256(usuarioRequestDto.Contrasena);
            await _context.Usuarios.InsertOneAsync(usuario);

            response.Mensaje = "Usuario creado exitosamente";
            response.Estado = true;

            if (usuario.Id == null)
            {
                response.Mensaje = "Fallo al crear el usuario";
                response.Estado = false;
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

    public async Task<Response<dynamic?>> InicioUsuario(LoginRequestDTO loginRequestDto)
    {
        Response<dynamic?> response = new();
        try
        {
            var buscarUsuario = await _context.Usuarios
                .Find(u => u.Correo == loginRequestDto.Correo &&
                           u.Contrasena == _utilidades.EncriptarSHA256(loginRequestDto.Contrasena))
                .SingleOrDefaultAsync();

            if (buscarUsuario == null)
            {
                response.Estado = false;
                response.Mensaje = "No se ha podido validar las credenciales";
            }
            else
            {
                string token = _utilidades.GenerarJWT(buscarUsuario);
                UsuarioResponseDTO usuarioResponseDto = _mapper.Map<UsuarioResponseDTO>(buscarUsuario);
                usuarioResponseDto.Token = token;
                var listaFormularios = await _formularioRepository.FormulariosDeUnUsuario(usuarioResponseDto.Id);
                usuarioResponseDto.FormulariosBody = listaFormularios.Cuerpo;
                response.Estado = true;
                response.Mensaje = "Inicio de sesion exitoso";
                response.Cuerpo = usuarioResponseDto;
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
}