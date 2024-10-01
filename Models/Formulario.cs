using System.Text.Json.Serialization;
using BlackFormBackend.Models.Categorias;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models;

public class Formulario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public required DateTime Fecha_Creacion { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TematicaEnum Tematica { get; set; }
    public string? Url { get; set; }
    public string? ImagenReferencia { get; set; }
    public required bool esPublico { get; set; }
    public required bool esActivo { get; set; }
    public required string UsuarioId { get; set; }
    public List<ICategoria> Categoria { get; set; }

    public Formulario()
    {
        Url = $"form/{Guid.NewGuid()}/viewform";
    }
    
}