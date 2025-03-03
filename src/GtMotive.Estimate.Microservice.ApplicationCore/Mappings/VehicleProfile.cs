using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Request;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Mappings
{
    /// <summary>
    /// Vehicle profile for mapping.
    /// </summary>
    public class VehicleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleProfile"/> class.
        /// </summary>
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleRequest>()
                .ForMember(dest => dest.MakeDate, opt => opt.MapFrom(src => src.MakeDate))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<VehicleRequest, Vehicle>()
                .ForMember(dest => dest.MakeDate, opt => opt.MapFrom(src => src.MakeDate))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RentedByClientId, opt => opt.Ignore()); // No mapeamos RentedByClientId porque no está en VehicleRequest
        }
    }
}
