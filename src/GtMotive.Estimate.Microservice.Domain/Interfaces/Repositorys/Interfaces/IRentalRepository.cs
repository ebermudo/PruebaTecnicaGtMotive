using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces.Repositorys.Interfaces
{
    /// <summary>
    /// Rental repository.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Function to create a Rental.
        /// </summary>
        /// <param name="rental">Rental to create.</param>
        /// <returns>true if can created, false if not.</returns>
        Task<bool> CreateRental(Rental rental);

        /// <summary>
        /// Function to update a Rental.
        /// </summary>
        /// <param name="rental">Rental to update.</param>
        /// <returns>true if can updated, false if not.</returns>
        Task<bool> UpdateRental(Rental rental);

        /// <summary>
        /// Validate if client has active rentals.
        /// </summary>
        /// <param name="clientId">client Id.</param>
        /// <returns>true if has active rental, false if not.</returns>
        public Task<bool> HasActiveRental(int clientId);

        /// <summary>
        /// Function for gets the rental.
        /// </summary>
        /// <param name="vehicleId">vehicle Id.</param>
        /// <returns>the rental founded.</returns>
        public Task<Rental> GetRentalByVehicleId(int vehicleId);
    }
}
