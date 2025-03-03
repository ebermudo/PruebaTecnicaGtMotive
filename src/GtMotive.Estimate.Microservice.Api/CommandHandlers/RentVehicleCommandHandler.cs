using GtMotive.Estimate.Microservice.ApplicationCore.Commands;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Api.CommandHandlers
{
    public class RentVehicleCommandHandler : IRequestHandler<RentVehicleCommand, bool>
    {
        private readonly IVehicleRentalService _vehicleRentalService;

        public RentVehicleCommandHandler(IVehicleRentalService vehicleRentalService)
        {
            _vehicleRentalService = vehicleRentalService;
        }

        public async Task<bool> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        {
            return request == null
                ? throw new ArgumentNullException(nameof(request), "El parámetro request no puede ser nulo.")
                : await _vehicleRentalService.RentVehicle(request.ClientId, request.VehicleId);
        }
    }
}
