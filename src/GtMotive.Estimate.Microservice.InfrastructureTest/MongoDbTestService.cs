using GtMotive.Estimate.Microservice.Domain.Entities;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.InfrastructureTests
{
    public class MongoDbTestService
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbTestService(string connectionString, string databaseName)
        {
            _mongoClient = new MongoClient(connectionString);
            _mongoDatabase = _mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<Client> GetClientsCollection()
        {
            return _mongoDatabase.GetCollection<Client>("Clients");
        }

        // Otros métodos que necesites para interactuar con la base de datos de prueba
    }
}
