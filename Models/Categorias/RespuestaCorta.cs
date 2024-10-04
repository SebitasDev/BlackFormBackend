using System.Text.Json.Serialization;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

[BsonDiscriminator("respuestaCorta")]
public class RespuestaCorta : Categoria
{
    public RespuestaCorta()
    {
        Id = Guid.NewGuid();
        TipoCategoria = TipoCategoriaEnum.RespuestaCorta;
    }
}