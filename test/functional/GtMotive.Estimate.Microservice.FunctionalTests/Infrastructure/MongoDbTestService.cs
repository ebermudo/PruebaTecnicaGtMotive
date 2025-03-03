using GtMotive.Estimate.Microservice.Domain.Entities;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
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

        public IMongoCollection<Vehicle> GetVehicleCollection()
        {
            return _mongoDatabase.GetCollection<Vehicle>("Vehicles");
        }
    }
}
