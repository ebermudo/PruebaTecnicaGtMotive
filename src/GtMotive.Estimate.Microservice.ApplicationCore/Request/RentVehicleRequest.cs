namespace GtMotive.Estimate.Microservice.ApplicationCore.Request
{
    /// <summary>
    /// Rent vehicle request.
    /// </summary>
    public class RentVehicleRequest
    {
        /// <summary>
        /// gets or sets the client Id.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// gets or sets the Vehicle Id.
        /// </summary>
        public int VehicleId { get; set; }
    }
}
