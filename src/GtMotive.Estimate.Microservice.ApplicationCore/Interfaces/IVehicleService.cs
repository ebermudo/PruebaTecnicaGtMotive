using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Request;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.PublicInterfaces
{
    /// <summary>
    /// Service for do actions of vehicles.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Create a vehicle.
        /// </summary>
        /// <param name="request">vehicle to create.</param>
        /// <returns>vehicle created.</returns>
        public Task<Vehicle> CreateVehicle(VehicleRequest request);

        /// <summary>
        /// Gets available vehicles.
        /// </summary>
        /// <returns>vehicle created.</returns>
        public Task<IEnumerable<Vehicle>> GetAvailableVehicles();
    }
}
