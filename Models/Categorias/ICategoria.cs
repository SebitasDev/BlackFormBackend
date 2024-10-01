using System.Text.Json.Serialization;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public interface ICategoria
{
    Guid Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    TipoCategoriaEnum TipoCategoria { get; set; }
    string? Pregunta { get; set; }
    bool esObligatoria { get; set; }
}