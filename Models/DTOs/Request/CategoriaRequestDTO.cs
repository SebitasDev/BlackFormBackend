using System.Text.Json.Serialization;
using BlackFormBackend.Models.Categorias;
using BlackFormBackend.Models.ENUMs;

namespace BlackFormBackend.Models.DTOs.Request;

public class CategoriaRequestDTO
{
    public TipoCategoriaEnum TipoCategoria { get; set; }
    public string? Pregunta { get; set; }
    public bool esObligatoria { get; set; }
    public List<Opcion>? Opciones { get; set; }

    public CategoriaRequestDTO()
    {
        Opciones = null;
    }
}