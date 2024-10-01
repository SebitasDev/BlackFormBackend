using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models.Categorias;

public class Opcion
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }

    public Opcion()
    {
        Id = Guid.NewGuid();
    }
}