using System;
using GtMotive.Estimate.Microservice.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Entity for rental vehicles.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        public Rental()
        {
        }

        /// <summary>
        /// gets or sets Id of mongo document.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the VehicleId.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the rentalStatus.
        /// </summary>
        public RentalStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the RentalDate.
        /// </summary>
        public DateTime RentalDate { get; set; }

        /// <summary>
        /// Gets or sets the EndDate.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
