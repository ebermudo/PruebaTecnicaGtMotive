using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Clase vehiculo para obtener a partir del command.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="makeDate">set the makeDate.</param>
        /// <param name="id">set the id.</param>
        /// <param name="rentedByClientId">set the rentedByClientId.</param>
        /// <param name="isAvailable">set the isAvailable.</param>
        /// <param name="name">set the name.</param>
        public Vehicle(string makeDate, int id, string name, bool isAvailable, int? rentedByClientId)
        {
            Id = id;
            Name = name;
            IsAvailable = isAvailable;
            RentedByClientId = rentedByClientId;
            MakeDate = makeDate;
        }

        /// <summary>
        /// gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the makeDate.
        /// </summary>
        public string MakeDate { get; set; }

        /// <summary>
        /// gets or sets a value indicating whether is available vehicle.
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// gets or sets name of vehicle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// gets or sets value of its rented by clientId.
        /// </summary>
        public int? RentedByClientId { get; set; }

        /// <summary>
        /// Function for rent a vehicle.
        /// </summary>
        /// <param name="clientId">client id.</param>
        /// <exception cref="InvalidOperationException">Exception for vehicle not available.</exception>
        public void Rent(int clientId)
        {
            if (!IsAvailable)
            {
                throw new InvalidOperationException("El vehículo no está disponible.");
            }

            IsAvailable = false;
            RentedByClientId = clientId;
        }
    }
}
