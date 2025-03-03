using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repository.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repository.Implementations
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicleCollection;

        // Inyectamos MongoService y obtenemos la colección de vehículos
        public VehicleRepository(MongoService mongoService)
        {
            if (mongoService == null)
            {
                throw new ArgumentNullException(nameof(mongoService), "El parámetro mongoService no puede ser nulo.");
            }

            _vehicleCollection = mongoService.GetCollection<Vehicle>("Vehicles");
        }

        /// <summary>
        /// Creates a new vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle entity.</param>
        /// <returns>The created vehicle.</returns>
        public async Task<Vehicle> CreateVehicle(Vehicle vehicle)
        {
            await _vehicleCollection.InsertOneAsync(vehicle);
            return vehicle;
        }

        /// <summary>
        /// Gets all available vehicles.
        /// </summary>
        /// <returns>A list of available vehicles.</returns>
        public async Task<IEnumerable<Vehicle>> GetAvailableVehicles()
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.IsAvailable, true);
            return await _vehicleCollection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Gets a vehicle by its ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID.</param>
        /// <returns>The vehicle if found, null otherwise.</returns>
        public async Task<Vehicle> GetById(int vehicleId)
        {
            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, vehicleId);
            return await _vehicleCollection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the vehicle information.
        /// </summary>
        /// <param name="vehicle">Vehicle entity.</param>
        /// <returns>void task.</returns>
        public async Task Update(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "El parámetro vehicle no puede ser nulo.");
            }

            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, vehicle.Id);
            var update = Builders<Vehicle>.Update
                .Set(v => v.IsAvailable, vehicle.IsAvailable)
                .Set(v => v.Name, vehicle.Name);

            await _vehicleCollection.UpdateOneAsync(filter, update);
        }
    }
}
