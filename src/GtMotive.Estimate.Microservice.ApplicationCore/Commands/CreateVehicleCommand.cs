using System;
using GtMotive.Estimate.Microservice.ApplicationCore.Request;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Commands
{
    /// <summary>
    /// Command para la el servicio de vehiculos.
    /// </summary>
    public class CreateVehicleCommand : IRequest<Vehicle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleCommand"/> class.
        /// </summary>
        /// <param name="vehicle">vehicle.</param>
        public CreateVehicleCommand(VehicleRequest vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "El parámetro vehicle no puede ser nulo.");
            }

            Vehicle = new Vehicle(vehicle.MakeDate, vehicle.Id, vehicle.Name, vehicle.IsAvailable, null);
        }

        /// <summary>
        /// Gets or sets sthe Make Date of Vehicle.
        /// </summary>
        public Vehicle Vehicle { get; set; }
    }
}
