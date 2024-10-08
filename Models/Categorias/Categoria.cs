using System.Text.Json;
using System.Text.Json.Serialization;
using BlackFormBackend.Custom;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public class Categoria
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoCategoriaEnum TipoCategoria { get; set; }
    public string? Pregunta { get; set; }
    public bool esObligatoria { get; set; }
    public List<Opcion>? Opciones { get; set; }

    public Categoria()
    {
        Id = Guid.NewGuid();
    }
}
