using System.Text.Json.Serialization;
using BlackFormBackend.Custom;
using BlackFormBackend.Models.Categorias;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.DTOs.Request;

public class FormularioRequestDTO
{
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public required DateTime Fecha_Creacion { get; set; }
    public TematicaEnum Tematica { get; set; }
    public string? ImagenReferencia { get; set; }
    public required bool esPublico { get; set; }
    public required bool esActivo { get; set; }
    public required string UsuarioId { get; set; }
    public List<Categoria> Categoria { get; set; }

    public FormularioRequestDTO()
    {
        Fecha_Creacion = DateTime.Now;
    }
    
    
}