using BlackFormBackend.Models.DTOs.Request;
using BlackFormBackend.Models.DTOs.Response;
using Microsoft.AspNetCore.Identity.Data;

namespace BlackFormBackend.Repository.Acceso;

public interface IAccesoRepository
{
    Task<Response<string?>> RegistroUsuario(UsuarioRequestDTO usuarioRequestDto);
    Task<Response<dynamic?>> InicioUsuario(LoginRequestDTO loginRequestDto);
}