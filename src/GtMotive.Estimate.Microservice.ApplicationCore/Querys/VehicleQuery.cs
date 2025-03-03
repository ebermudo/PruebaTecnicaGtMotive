using System.Collections.Generic;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Querys
{
    /// <summary>
    /// Gets all the available vehicles.
    /// </summary>
    public class VehicleQuery : IRequest<IEnumerable<Vehicle>>
    {
    }
}
