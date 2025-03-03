using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.PublicInterfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.Request;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repository.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.PublicImplementations
{
    /// <summary>
    /// Service for do actions of vehicles.
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleService"/> class.
        /// </summary>
        /// <param name="vehicleRepository">vehicle repository to call.</param>
        /// <param name="mapper">interface for mapping.</param>
        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a vehicle.
        /// </summary>
        /// <param name="request">vehicle to create.</param>
        /// <returns>vehicle created.</returns>
        public async Task<Vehicle> CreateVehicle(VehicleRequest request)
        {
            var result = await _vehicleRepository.CreateVehicle(_mapper.Map<Vehicle>(request));
            return result;
        }

        /// <summary>
        /// Gets available vehicles.
        /// </summary>
        /// <returns>vehicle created.</returns>
        public async Task<IEnumerable<Vehicle>> GetAvailableVehicles()
        {
            var result = await _vehicleRepository.GetAvailableVehicles();
            return result;
        }
    }
}
