using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Commands
{
    /// <summary>
    /// Command for rent a vehicle.
    /// </summary>
    public class RentVehicleCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleCommand"/> class.
        /// </summary>
        /// <param name="clientId">id of client.</param>
        /// <param name="vehicleId">id of vehicle.</param>
        public RentVehicleCommand(int clientId, int vehicleId)
        {
            ClientId = clientId;
            VehicleId = vehicleId;
        }

        /// <summary>
        /// Gets id of the client.
        /// </summary>
        public int ClientId { get; }

        /// <summary>
        /// Gets id of the vehicle.
        /// </summary>
        public int VehicleId { get; }
    }
}
