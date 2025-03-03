using GtMotive.Estimate.Microservice.ApplicationCore.PublicInterfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.Querys;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Api.QueryHandler
{
    public class VehicleQueryHandler : IRequestHandler<VehicleQuery, IEnumerable<Vehicle>>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IEnumerable<Vehicle>> Handle(VehicleQuery request, CancellationToken cancellationToken)
        {
            return await _vehicleService.GetAvailableVehicles();
        }
    }
}
