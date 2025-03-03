using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MongoDB.Driver;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure
{
    public class MongoDbTest : IClassFixture<TestMongoDbServiceFixture>
    {
        private readonly MongoDbTestService _mongoDbTestService;
        private readonly IMongoCollection<Client> _clientCollection;
        private readonly IMongoCollection<Vehicle> _vehicleCollection;

        public MongoDbTest(TestMongoDbServiceFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException(nameof(fixture));
            }

            _mongoDbTestService = fixture.MongoDbTestService;

            if (_mongoDbTestService == null)
            {
                throw new ArgumentNullException(nameof(fixture), "El servicio MongoDbTestService no puede ser nulo.");
            }

            // Usamos el servicio para obtener la colección de clientes
            _clientCollection = _mongoDbTestService.GetClientsCollection();
            _vehicleCollection = _mongoDbTestService.GetVehicleCollection();
        }

        [Fact]
        public void TestDatabaseConnection()
        {
            var clients = _clientCollection.Find(client => true).ToList();
            Assert.NotEmpty(clients);
        }

        [Fact]
        public void TestCreateVehicle()
        {
            _vehicleCollection.InsertOne(new Vehicle
            {
                Id = 4,
                IsAvailable = true,
                MakeDate = "01/03/2025",
                Name = "ford scort"
            });
            var vehicleCreated = _vehicleCollection.Find(vehicle => vehicle.Id == 4).ToList();
            Assert.NotEmpty(vehicleCreated);
        }
    }
}
