using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Commands;
using GtMotive.Estimate.Microservice.ApplicationCore.Querys;
using GtMotive.Estimate.Microservice.ApplicationCore.Request;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleController"/> class.
        /// </summary>
        /// <param name="mediator">mediator interface.</param>
        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new vehicle.
        /// </summary>
        /// <param name="request">vehicle to create.</param>
        /// <returns>Vehicle created.</returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleRequest request)
        {
            var command = new CreateVehicleCommand(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Gets the available vehicles.
        /// </summary>
        /// <returns>list of available vehicles.</returns>
        [HttpGet("GetAvailableVehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAvailableVehicles()
        {
            var result = await _mediator.Send(new VehicleQuery());
            return Ok(result);
        }

        /// <summary>
        /// Function for Rent a vehicle.
        /// </summary>
        /// <param name="request">rent vehicle request with clientId and vehicleId.</param>
        /// <returns>a true or false for vehicle rented.</returns>
        /// <exception cref="ArgumentNullException">param request cannot be null.</exception>
        [HttpPost("RentVehicle")]
        public async Task<IActionResult> RentVehicle([FromBody] RentVehicleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "El parámetro request no puede ser nulo.");
            }

            var command = new RentVehicleCommand(request.ClientId, request.VehicleId);
            var success = await _mediator.Send(command);

            return success ? Ok("Vehículo alquilado con éxito") : BadRequest("Error al alquilar el vehículo");
        }

        /// <summary>
        /// Return a vehicle.
        /// </summary>
        /// <param name="request">vehicle to return and client got the vehicle.</param>
        /// <returns>actionResult of vehicle returned.</returns>
        [HttpPost("ReturnVehicle")]
        public async Task<IActionResult> ReturnVehicle([FromBody] RentVehicleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "El parámetro request no puede ser nulo.");
            }

            var command = new ReturnVehicleCommand(request.ClientId, request.VehicleId);
            var success = await _mediator.Send(command);

            return success ? Ok("Vehículo devuelto con éxito") : BadRequest("Error al devolver el vehículo");
        }
    }
}
