using BlackFormBackend.Models.Categorias;

namespace BlackFormBackend.Models.DTOs.Request;

public class CategoriaUpdateRequestDTO
{
    public string? Pregunta { get; set; }
    public List<Opcion>? Opciones { get; set; }
}