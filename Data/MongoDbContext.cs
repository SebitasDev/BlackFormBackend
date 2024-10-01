using BlackFormBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BlackFormBackend.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<DatabaseSettings> databaseSettings)
    {
        var settings = MongoClientSettings.FromUrl(new MongoUrl(databaseSettings.Value.ConnectionString));
        
        settings.SslSettings = new SslSettings
        {
            CheckCertificateRevocation = false,
            EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
        };

        settings.ConnectTimeout = TimeSpan.FromSeconds(30);
        settings.SocketTimeout = TimeSpan.FromSeconds(30);

        MongoClient mongoClient = new MongoClient(settings);
        _database = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
    }

    public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>("Usuarios");
    public IMongoCollection<Formulario> Formularios => _database.GetCollection<Formulario>("Formularios");
}