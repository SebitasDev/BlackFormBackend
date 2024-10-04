using BlackFormBackend.Models.ENUMs;

namespace BlackFormBackend.Models.DTOs.Request;

public class FormularioUpdateRequestDTO
{
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public TematicaEnum Tematica { get; set; }
    public string? ImagenReferencia { get; set; }
}