// <copyright file="IVehicleRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Repository.Interfaces
{
    /// <summary>
    /// Vehicle repository.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Function to create a new vehicle.
        /// </summary>
        /// <param name="vehicle">vehicle to create.</param>
        /// <returns>Vehicle created.</returns>
        public Task<Vehicle> CreateVehicle(Vehicle vehicle);

        /// <summary>
        /// Gets available vehicles.
        /// </summary>
        /// <returns>List of available vehicles.</returns>
        public Task<IEnumerable<Vehicle>> GetAvailableVehicles();

        /// <summary>
        /// Get vehicle by Id.
        /// </summary>
        /// <param name="vehicleId">vehicle Id.</param>
        /// <returns>the vehicle getted.</returns>
        public Task<Vehicle> GetById(int vehicleId);

        /// <summary>
        /// Update the vehicle.
        /// </summary>
        /// <param name="vehicle">vehicle for update.</param>
        /// <returns>a task void.</returns>
        public Task Update(Vehicle vehicle);
    }
}
