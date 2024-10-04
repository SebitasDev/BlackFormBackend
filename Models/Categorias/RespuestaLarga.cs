using System.Text.Json.Serialization;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public class RespuestaLarga : Categoria
{
    [JsonConstructor]
    public RespuestaLarga() : base()
    {
        Id = Guid.NewGuid();
        TipoCategoria = TipoCategoriaEnum.RespuestaLarga;
    }
}