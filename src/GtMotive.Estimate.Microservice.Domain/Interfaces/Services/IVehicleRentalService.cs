using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces.Services
{
    /// <summary>
    /// rental service.
    /// </summary>
    public interface IVehicleRentalService
    {
        /// <summary>
        /// Function for rent a vehicle.
        /// </summary>
        /// <param name="clientId">client id.</param>
        /// <param name="vehicleId">vehicle id.</param>
        /// <returns>a true or false for vehicle rented.</returns>
        public Task<bool> RentVehicle(int clientId, int vehicleId);

        /// <summary>
        /// Function for validate if vehicle can be created.
        /// </summary>
        /// <param name="vehicle">vehicle to validate.</param>
        /// <returns>a true if vehicle can be created, false if not.</returns>
        public bool CanCreateVehicle(Vehicle vehicle);

        /// <summary>
        /// function for return a vehicle.
        /// </summary>
        /// <param name="clientId">client Id.</param>
        /// <param name="vehicleId">vehicle Id.</param>
        /// <returns>true if its returned, false if not.</returns>
        public Task<bool> ReturnVehicle(int clientId, int vehicleId);
    }
}
