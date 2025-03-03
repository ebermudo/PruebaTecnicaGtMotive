namespace GtMotive.Estimate.Microservice.ApplicationCore.Request
{
    /// <summary>
    /// Vehicle Request.
    /// </summary>
    public class VehicleRequest
    {
        /// <summary>
        /// gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the makeDate.
        /// </summary>
        public string MakeDate { get; set; }

        /// <summary>
        /// gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// gets or sets a value indicating whether is available vehicle.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
