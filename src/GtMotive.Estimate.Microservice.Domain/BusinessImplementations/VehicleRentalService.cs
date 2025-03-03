// <copyright file="VehicleRentalService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Repositorys.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Services;
using GtMotive.Estimate.Microservice.Domain.Repository.Interfaces;

namespace GtMotive.Estimate.Microservice.Domain.Implementations
{
    /// <summary>
    /// Service domain for RENT vehicles.
    /// </summary>
    public class VehicleRentalService : IVehicleRentalService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRentalService"/> class.
        /// </summary>
        /// <param name="clientRepository">client repository for data.</param>
        /// <param name="vehicleRepository">vehicle repository for data.</param>
        /// <param name="rentalRepository">rental repository for data.</param>
        public VehicleRentalService(IVehicleRepository vehicleRepository, IClientRepository clientRepository, IRentalRepository rentalRepository)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Function for rent the vehicle.
        /// </summary>
        /// <param name="clientId">client id for rent the vehicle.</param>
        /// <param name="vehicleId">vehicle id of the rent.</param>
        /// <returns>true if rented, false if not.</returns>
        /// <exception cref="InvalidOperationException">exception in case can't rent the vehicle.</exception>
        public async Task<bool> RentVehicle(int clientId, int vehicleId)
        {
            var client = await _clientRepository.GetById(clientId).ConfigureAwait(false);

            if (client != null)
            {
                var hasActiveRental = await _rentalRepository.HasActiveRental(client.ClientId).ConfigureAwait(false);
                if (hasActiveRental)
                {
                    throw new InvalidOperationException("El cliente ya tiene un vehículo alquilado.");
                }

                var vehicle = await _vehicleRepository.GetById(vehicleId).ConfigureAwait(false);
                if (vehicle == null || !vehicle.IsAvailable)
                {
                    throw new InvalidOperationException("El vehículo no está disponible para alquiler.");
                }

                vehicle.Rent(clientId);

                await _vehicleRepository.Update(vehicle).ConfigureAwait(false);
                var rental = new Rental
                {
                    VehicleId = vehicleId,
                    ClientId = clientId,
                    Status = Enums.RentalStatus.Active,
                    RentalDate = DateTime.UtcNow
                };

                await _rentalRepository.CreateRental(rental);

                return true;
            }

            throw new InvalidOperationException("El cliente no existe.");
        }

        /// <summary>
        /// Function for validate if vehicle can be created for the business rule more than 5 years.
        /// </summary>
        /// <param name="vehicle">vehicle to create.</param>
        /// <returns>true if can be created, false if not.</returns>
        public bool CanCreateVehicle(Vehicle vehicle)
        {
            return vehicle == null
                ? throw new ArgumentNullException(nameof(vehicle), "El parámetro vehicle no puede ser nulo.")
                : DateTime.TryParse(vehicle.MakeDate, out var makeDate) && makeDate.AddYears(5) >= DateTime.UtcNow;
        }

        /// <summary>
        /// Return a vehicle.
        /// </summary>
        /// <param name="clientId">client Id.</param>
        /// <param name="vehicleId">Vehicle Id.</param>
        /// <returns>returns true if its returned, false if not.</returns>
        /// <exception cref="InvalidOperationException">exception for vehicle not rented for this client or not exist.</exception>
        public async Task<bool> ReturnVehicle(int clientId, int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId).ConfigureAwait(false);
            var rental = await _rentalRepository.GetRentalByVehicleId(vehicleId).ConfigureAwait(false);
            if (rental == null || rental.Status != Enums.RentalStatus.Active)
            {
                throw new InvalidOperationException("El vehículo no está alquilado por este cliente o no existe.");
            }

            vehicle.IsAvailable = true;
            vehicle.RentedByClientId = null;

            await _vehicleRepository.Update(vehicle).ConfigureAwait(false);
            rental.Status = Enums.RentalStatus.Returned;
            await _rentalRepository.UpdateRental(rental).ConfigureAwait(false);

            return true;
        }
    }
}
