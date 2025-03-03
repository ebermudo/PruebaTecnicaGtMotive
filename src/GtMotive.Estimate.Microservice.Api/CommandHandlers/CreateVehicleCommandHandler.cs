using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Commands;
using GtMotive.Estimate.Microservice.ApplicationCore.PublicInterfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.Request;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces.Services;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.CommandHandlers
{
    /// <summary>
    /// Handler for VehicleCommands.
    /// </summary>
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Vehicle>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleRentalService _vehicleRentalService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleService">service of vehicles.</param>
        /// <param name="vehicleRentalService">service of business domain rental vehicles.</param>
        /// <param name="mapper">interface for mapping.</param>
        public CreateVehicleCommandHandler(IVehicleService vehicleService, IVehicleRentalService vehicleRentalService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _vehicleRentalService = vehicleRentalService;
            _mapper = mapper;
        }

        /// <summary>
        /// Handle the request of command.
        /// </summary>
        /// <param name="request">vehicle to set.</param>
        /// <param name="cancellationToken"> cancelationToken.</param>
        /// <returns>Vehicle setted.</returns>
        public async Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "El parámetro request no puede ser nulo.");
            }

            var vehicle = request.Vehicle;
            if (!_vehicleRentalService.CanCreateVehicle(vehicle))
            {
                throw new InvalidOperationException("No se pueden agregar vehículos con más de 5 años de antigüedad.");
            }

            var vehicleRequest = _mapper.Map<VehicleRequest>(vehicle);

            var success = await _vehicleService.CreateVehicle(vehicleRequest);
            return success ?? vehicle;
        }
    }
}
