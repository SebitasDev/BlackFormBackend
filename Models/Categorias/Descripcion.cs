using System.Text.Json.Serialization;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public class Descripcion : Categoria
{
    public required string Titulo { get; set; }
    public required string DescripcionCategoria { get; set; }
    
    public Descripcion()
    {
        TipoCategoria = TipoCategoriaEnum.Descripcion;
        esObligatoria = false;
        Pregunta = null;
    }
}