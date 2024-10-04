using System.Text.Json.Serialization;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public class SeleccionMultiple : Categoria
{
    public List<Opcion> Opciones { get; set; }
    
    [JsonConstructor]
    public SeleccionMultiple() : base()
    {
        Id = Guid.NewGuid();
        TipoCategoria = TipoCategoriaEnum.SeleccionMultiple;
        Opciones = [];
    }
}