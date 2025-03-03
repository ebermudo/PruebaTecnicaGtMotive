using GtMotive.Estimate.Microservice.ApplicationCore.Commands;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Api.CommandHandlers
{
    public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand, bool>
    {
        private readonly IVehicleRentalService _rentalService;

        public ReturnVehicleCommandHandler(IVehicleRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public async Task<bool> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            return request == null
                ? throw new ArgumentNullException(nameof(request), "El parámetro request no puede ser nulo.")
                : await _rentalService.ReturnVehicle(request.ClientId, request.VehicleId);
        }
    }
}
