using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlackFormBackend.Models;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public required string Nombre {get; set;}
    
    public required string NombreCompleto { get; set; }
    
    public required string Correo { get; set; }
    
    public required string Contrasena { get; set; }

    public List<string> Formularios { get; set; } = [];
}