using System.Text.Json.Serialization;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public class Hora : Categoria
{
    public Hora()
    {
        TipoCategoria = TipoCategoriaEnum.Fecha;
    }
}