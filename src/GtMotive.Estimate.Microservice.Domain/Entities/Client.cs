using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Clients who rent the cars.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="makeDate">set the makeDate.</param>
        public Client(string makeDate)
        {
            MakeDate = makeDate;
        }

        /// <summary>
        /// gets or sets Id of mongo document.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// gets or sets the Id.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// gets or sets the makeDate.
        /// </summary>
        public string MakeDate { get; set; }

        /// <summary>
        /// gets or sets the dni.
        /// </summary>
        public string Dni { get; set; }

        /// <summary>
        /// gets or sets a value of vehicleId if he got one reserved.
        /// </summary>
        public int? VehicleId { get; set; }

        /// <summary>
        /// gets or sets a value of name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// gets or sets a value indicating whether he hasActiveRental.
        /// </summary>
        public bool HasActiveRental { get; set; }
    }
}
