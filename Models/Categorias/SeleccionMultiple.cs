using System.Text.Json.Serialization;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public class SeleccionMultiple : Categoria
{
    public List<Opcion> Opciones { get; set; }

    public SeleccionMultiple()
    {
        TipoCategoria = TipoCategoriaEnum.SeleccionMultiple;
        Opciones = [];
    }
}