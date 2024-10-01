namespace BlackFormBackend.Models.DTOs.Request;

public class LoginRequestDTO
{
    public required string Correo { get; set; }
    public required string Contrasena { get; set; }
}