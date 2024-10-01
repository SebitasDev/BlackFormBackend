using System.Text.Json.Serialization;
using BlackFormBackend.Models.ENUMs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public class SeleccionUnica : Categoria
{
    public List<Opcion> Opciones { get; set; }

    public SeleccionUnica()
    {
        TipoCategoria = TipoCategoriaEnum.SeleccionUnica;
        Opciones = [];
    }
}