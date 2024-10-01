namespace BlackFormBackend.Models.DTOs.Response;

public class UsuarioResponseDTO
{
    public string? Id { get; set; }
    
    public required string Nombre {get; set;}
    
    public required string NombreCompleto { get; set; }
    
    public required string Correo { get; set; }
    
    public string Token { get; set; }

    public List<FormularioResponseDTO>? FormulariosBody { get; set; } = [];
}