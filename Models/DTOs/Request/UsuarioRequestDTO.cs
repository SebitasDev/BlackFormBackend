namespace BlackFormBackend.Models.DTOs.Request;

public class UsuarioRequestDTO
{
    public required string Nombre { get; set; }
    public required string NombreCompleto { get; set; }
    public required string Correo { get; set; }
    public required string Contrasena { get; set; }
}