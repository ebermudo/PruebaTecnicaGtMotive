using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositorys.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repository.Implementations
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<Rental> _rentalCollection;

        public RentalRepository(MongoService mongoService)
        {
            if (mongoService == null)
            {
                throw new ArgumentNullException(nameof(mongoService), "El parámetro mongoService no puede ser nulo.");
            }

            _rentalCollection = mongoService.GetCollection<Rental>("Rentals");
        }

        /// <summary>
        /// Function for validate if client has an active rental.
        /// </summary>
        /// <param name="clientId">Client Id.</param>
        /// <returns>true if has an active rental, false otherwise.</returns>
        public async Task<bool> HasActiveRental(int clientId)
        {
            var filter = Builders<Rental>.Filter.And(
                Builders<Rental>.Filter.Eq(c => c.ClientId, clientId),
                Builders<Rental>.Filter.Eq(c => c.Status, RentalStatus.Active));
            var client = await _rentalCollection.Find(filter).FirstOrDefaultAsync();
            return client != null && client.Status == RentalStatus.Active;
        }

        public async Task<Rental> GetRentalByVehicleId(int vehicleId)
        {
            var filter = Builders<Rental>.Filter.And(
                Builders<Rental>.Filter.Eq(c => c.VehicleId, vehicleId),
                Builders<Rental>.Filter.Eq(c => c.Status, RentalStatus.Active));
            var rental = await _rentalCollection.Find(filter).FirstOrDefaultAsync();
            return rental;
        }

        public async Task<bool> CreateRental(Rental rental)
        {
            if (rental == null)
            {
                throw new ArgumentNullException(nameof(rental), "El parámetro rental no puede ser nulo.");
            }

            await _rentalCollection.InsertOneAsync(rental);
            return true;
        }

        public async Task<bool> UpdateRental(Rental rental)
        {
            if (rental == null)
            {
                return false;
                throw new ArgumentNullException(nameof(rental), "El parámetro rental no puede ser nulo.");
            }

            var filter = Builders<Rental>.Filter.Eq(r => r.ClientId, rental.ClientId) &
                         Builders<Rental>.Filter.Eq(r => r.VehicleId, rental.VehicleId) &
                         Builders<Rental>.Filter.Eq(r => r.Status, RentalStatus.Active);

            var update = Builders<Rental>.Update
                .Set(r => r.Status, RentalStatus.Returned)
                .Set(r => r.EndDate, DateTime.UtcNow);

            await _rentalCollection.UpdateOneAsync(filter, update);
            return true;
        }
    }
}
