namespace GtMotive.Estimate.Microservice.Domain.Enums
{
    /// <summary>
    /// Rental status.
    /// </summary>
    public enum RentalStatus
    {
        /// <summary>
        /// Alquiler en curso.
        /// </summary>
        Active,

        /// <summary>
        /// Vehiculo devuelto.
        /// </summary>
        Returned,

        /// <summary>
        /// Alquiler cancelado antes de ser completado.
        /// </summary>
        Cancelled
    }
}
